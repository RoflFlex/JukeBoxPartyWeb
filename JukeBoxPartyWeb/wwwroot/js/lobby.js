"use strict";
let queue = [];
let tracks_collection = [];
let songs;
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const roomName = urlParams.get('id');
//songs = await getTracks();

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.serverTimeoutInMilliseconds = 1000000;





//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
//Disable the send button until connection is established.
document.getElementById("select").disabled = true;

connection.on("ReceiveMessage", function (user, message) {

    addToChatbox(`${user} says ${message}`);
});
connection.on("JoinedRoom", function (message) {
    addToChatbox(`${message}`);
});
connection.on("LeftRoom", function ( message) {
    addToChatbox(`${message}`);
});



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

setInterval(function () {
    if (queue.length > 0) {
        connection.invoke("IsNextTrack").catch(function (err) {
            return console.error(err.toString());
        });

    }
}, 1000);

connection.on("IsNextTrack", function (boo) {
    console.log(boo);
    });

document.getElementById("sendButton").addEventListener("click", function (event) {
    //var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", roomName,"Someone", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("audio").addEventListener("ended", onTrackEnded)

document.getElementById("addTrackBtn").addEventListener("click", async function (event) {
    songs = await getTracks();
    console.log(songs);
    songs.forEach(e => tracks_collection.push(e['URL']));
    makeTracklist();
    event.preventDefault();
})
connection.on("ReceiveTrack", onRecievedTrack);
connection.on("TrackEnded", function () {
    console.log("Sign that track is ended");
})

async function getTracks() {
    var requestOptions = {
        method: 'GET',
        redirect: 'follow'
    };
    const tracks = await fetch("https://localhost:7283/api/Songs");
    return await tracks.json();
}

function onRecievedTrack(name, url) {
    console.log("track received");
    queue.push({
        name: name,
        id: url,
    });
    onSelectedTrack();
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
        //queue.push(selected.innerText);

        connection.invoke("SendTrack", selected.innerText, selected.getAttribute("data-internalid")).then(() => {
            console.log("invoked");

        }).catch(function (err) {
            return console.error(err.toString());
        });
        console.log(queue);
        //onSelectedTrack();
    } else {
        console.log("Please select an item from the list.");
    }
}

function onSelectedTrack() {
    makePlaylist();
    if (queue.length == 1) setCurrentTrack();
    if (queue.length == 2)    setNextTrack();
    
    
}


function makePlaylist() {
    const list = document.getElementById("playlist");
    list.innerHTML = "";
    queue.forEach(track => {
        const li = document.createElement("li");
        li.innerHTML = track.name;
        list.appendChild(li);

    })
}

function makeTracklist() {
    const list = document.getElementById("tracklist");
    list.innerHTML = "";
    songs.forEach(track => {
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

function onTrackEnded() {
    queue.splice(0, 1);
    if (queue[0]) {
        setCurrentTrack();
        setNextTrack();
    }
}


function setCurrentTrack() {
    if (queue[0]) {
        document.getElementById("currenttrack").innerHTML = queue[0].name;

        addTrackToAudio();

    }
}

function getURL(id) {
    const SONG = songs.find((element) => element.id == id);
    return SONG.url;
}

function addTrackToAudio() {
    const audio = document.getElementById("audio");
    const source = document.createElement("source");
    source.src = `/media/music/${getURL(queue[0].id)}`;
    source.type = "audio/mpeg";
    audio.appendChild(source);

}

function setNextTrack() {
    if (queue[1]) {
        document.getElementById("nexttrack").innerHTML = queue[1].name;
    }
}

window.onload = function () {
    //console.log(document.getElementById("select"));
    document.getElementById("select").addEventListener("click", selectItem);
    makeTracklist();
}