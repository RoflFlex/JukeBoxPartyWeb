"use strict";
let playlist = [];
let tracklist;
let users;
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const roomName = urlParams.get('id');
const baseurl = "https://localhost:7283/api/";
//songs = await getTracks();

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();


window.onload = async function () {
    //console.log(document.getElementById("select"));
    tracklist = await getTracks();
    await updateDashboard();
    if (playlist!=null && playlist.length > 0) {
        
        if (playlist[0].playedAt) {
            
            //let providedtime = 
            let difference = (new Date()).getTime() - Date.parse(playlist[0].playedAt);
            setTrackAudio(difference / 1000);
        } else {
            connection.invoke("SwitchTrack", roomName, playlist[0].id.toString()).catch(function (err) {
                return console.error(err.toString());
            });
        }

    }
    makeTracklist();

    document.getElementById("select").addEventListener("click", selectItem);
}



//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
//Disable the send button until connection is established.
document.getElementById("select").disabled = true;

connection.on("ReceiveMessage", function (user, message) {

    addToChatbox(`${user}: ${message}`);
});
connection.on("JoinedRoom",  updateChat);
connection.on("LeftRoom",  updateChat);

async function updateChat(message, usercardsJson) {
    try {
        users = await JSON.parse(usercardsJson);
    } catch (error) {
        console.log(error.message);
        //usercardsJson = `[${usercardsJson}]`
        users = usercardsJson;
    } finally {
        addToChatbox(`${message}`);
        makeUserCards();
    }
    
    
}

window.onbeforeunload = () => {
    connection.invoke("LeaveRoom", roomName).catch(function (err) {
        return console.error(err.toString());
    });
};
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("select").disabled = false;
    console.log("Connection started");
    connection.invoke("JoinRoom", roomName).catch(function (err) {
        return console.error(err.toString());
    });

}).catch(function (err) {
    return console.error(err.toString());
});




document.getElementById("sendButton").addEventListener("click", function (event) {
    //var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    if (message) {
        message = removeCSS(message);
        message = encodeUserInput(message);

        connection.invoke("SendMessage", roomName, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    event.preventDefault();
});

document.getElementById("audio").addEventListener("ended",  onTrackEnded);

document.getElementById("addTrackBtn").addEventListener("click", async function (event) {
    tracklist = await getTracks();
    console.log(tracklist);
    //tracklist.forEach(e => playlist.push(e['URL']));
    makeTracklist();
    event.preventDefault();
})
connection.on("ReceiveTrack", onRecievedTrack);
connection.on("OnSwitchTrack", async function (id) {
    await updateDashboard();
    if (playlist[0]?.playedAt) {

        if (playlist.find(x => x.id == id)) {
            
            setTrackAudio();
            
        }
    }
    console.log("Sign that track is ended");
})



async function onRecievedTrack(json) {
    console.log("track received");
    await updateDashboard();
    const songjson = JSON.parse(json);
    if (playlist[0].song.id == songjson.id) {
        connection.invoke("SwitchTrack", roomName, playlist[0].id.toString()).catch(function (err) {
            return console.error(err.toString());
        });
        //setTrackAudio();
    }
    console.log(playlist);
   
    document.getElementById("audio").play();
}

function addToChatbox(message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);

    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${message}`;
}

function selectItem() {
    const list = document.getElementById("tracklist");
    const selected = list.querySelector("li.selected");
    if (selected) {
        console.log("Selected Item: " + selected.innerText);
        //playlist.push(selected.innerText);
        let json = tracklist.find(el => el.id == selected.getAttribute("data-internalid"));
        connection.invoke("SendTrack", roomName, JSON.stringify(json)).then(() => {
            console.log("invoked");

        }).catch(function (err) {
            return console.error(err.toString());
        });

        console.log(playlist);
        //onSelectedTrack();
    } else {
        console.log("Please select an item from the list.");
    }
}

function makeUserCards() {
    const usercards = document.getElementById("usercards");
    usercards.innerHTML = "";
    usercards.innerText = "";
    console.log(users);
    users.forEach(el => {
        let iets = new Usercard(el.NickName, el.Url);
        usercards.appendChild(iets);
    })
}

function makePlaylist() {
    const list = document.getElementById("playlist");
    list.innerHTML = "";
    if (playlist != null)
    playlist.forEach(track => {
        const li = document.createElement("li");
        li.innerHTML = track.song.artist + " - " + track.song.title;
        list.appendChild(li);

    });
}

function setDashboard() {

    setCurrentTrack();
    setNextTrack();
}

function makeTracklist() {
    const list = document.getElementById("tracklist");
    list.innerHTML = "";
    tracklist.forEach(track => {
        const li = document.createElement("li");
        li.innerHTML = `${track.artist} - ${track.title}`;
        li.setAttribute("data-internalid", track.id);
        list.appendChild(li);
    });
    attachEventsTracklist();
    
}

function attachEventsTracklist() {
    const items = document.querySelectorAll("#tracklist li");
    for (const item of items) {
        item.addEventListener("click", () => {
            const selected = document.querySelector("#tracklist li.selected");
            if (selected) {
                selected.classList.remove("selected");
            }
            item.classList.add("selected");
        });
    }
}

function selectTrack() {
    const selected = document.querySelector("#tracklist li.selected");
    if (selected) {
        selected.classList.remove("selected");
    }
    item.classList.add("selected");
}

async function onTrackEnded() {
    if (playlist[1])
        connection.invoke("SwitchTrack", roomName, playlist[1].id.toString()).catch(function (err) {
        return console.error(err.toString());
        });
    await updateDashboard();
}

async function updateDashboard() {
    playlist = await getPlayList(roomName);
    makePlaylist();
    setDashboard();
}

function setCurrentTrack() {
    if (playlist != null && playlist.length >= 1) {
        const track = playlist[0].song;
        document.getElementById("currenttrack").innerHTML = `${track.artist} - ${track.title}`;

    } else {
        document.getElementById("currenttrack").innerHTML = `Not selected yet!`;
    }
}

function getURL(id) {
    const SONG = tracklist.find((element) => element.id == id);
    return SONG.url;
}

function setTrackAudio(currenttime = 0) {
    const audio = document.getElementById("audio");
    const source = document.createElement("source");
    if (currenttime != 0) 
    audio.currentTime = Math.floor(currenttime); 
    audio.src = `/media/music/${playlist[0].song.url}`;
    audio.type = "audio/mpeg";
    //audio.appendChild(source);

}

async function getPlayList(lobbyId) {


    var requestOptions = {
        method: 'GET',
        redirect: 'follow'
    };

    var response = await fetch(`${baseurl}QueueElements/Lobby/${lobbyId}`, requestOptions);
    if (response.ok) {
        return await response.json();
    }
    return null;
}
async function getTracks() {
    var requestOptions = {
        method: 'GET',
        redirect: 'follow'
    };
    const tracks = await fetch("https://localhost:7283/api/Songs");
    return await tracks.json();
}


function setNextTrack() {
    if (playlist != null && playlist.length >= 2) {
        const track = playlist[1];
        document.getElementById("nexttrack").innerHTML = `${track.song.artist} - ${track.song.title}`;

    } else {
        document.getElementById("nexttrack").innerHTML = `Not selected yet!`;
    }

}
function removeCSS(input) {
    const regex = /<style([\s\S]*?)<\/style>|style\s*?=(['"])[\s\S]*?\2/gi;
    return input.replace(regex, '');
}
function encodeUserInput(input) {
    return encodeURIComponent(input);
}