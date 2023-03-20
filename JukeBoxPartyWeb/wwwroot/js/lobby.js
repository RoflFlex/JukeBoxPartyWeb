"use strict";
let queue = [];
let tracks_collection = [];
let songs;

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
//Disable the send button until connection is established.
document.getElementById("select").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says ${message}`;
});


connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("select").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    //var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", "Someone", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
document.getElementById("addTrackBtn").addEventListener("click", function (event) {
    songs = getTracks().then(function (users) {
        return users;
    });
    console.log(songs);
    songs.forEach(e => tracks_collection.push(e['URL']));
    makeTracklist();
    event.preventDefault();
})
connection.on("ReceiveTrack", onRecievedTrack);


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
    queue.push(url);
    onSelectedTrack();
    document.getElementById("audio").play();
}

function selectItem() {
    const list = document.getElementById("tracklist");
    const selected = list.querySelector("li.selected");
    if (selected) {
        console.log("Selected Item: " + selected.innerText);
        //queue.push(selected.innerText);

        connection.invoke("SendTrack", "LeukeNummer", selected.getAttribute("data-internalid")).then(() => {
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
    setCurrentTrack();
    setNextTrack();
    addTrackToAudio();
}


function makePlaylist() {
    const list = document.getElementById("playlist");
    list.innerHTML = "";
    queue.forEach(track => {
        const li = document.createElement("li");
        li.innerHTML = track;
        list.appendChild(li);
    })
}

function makeTracklist() {
    const list = document.getElementById("tracklist");
    list.innerHTML = "";
    songs.forEach(track => {
        const li = document.createElement("li");
        li.innerHTML = `${track.Artist} - ${track.Title}`;
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

function setCurrentTrack() {
    if (queue[queue.length - 1]) {
        document.getElementById("currenttrack").innerHTML = queue[queue.length - 1];
    }
}

function getURL(id) {
    const SONG = songs.find((element) => element.Id == id);
    return SONG.URL;
}

function addTrackToAudio() {
    const audio = document.getElementById("audio");
    const source = document.createElement("source");
    source.src = `/media/music/${queue[queue.length - 1]}`;
    source.type = "audio/mpeg";
    audio.appendChild(source);

}

function setNextTrack() {
    if (queue[1]) {
        document.getElementById("nexttrack").in = queue[1];
    }
}

window.onload = function () {
    //console.log(document.getElementById("select"));
    document.getElementById("select").addEventListener("click", selectItem);
    makeTracklist();
}