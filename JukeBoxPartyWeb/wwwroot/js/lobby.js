let queue = [];
const tracks_collection = ["track1.mp3", "track2.mp3", "track3.mp3"]


function selectItem() {
    const list = document.getElementById("tracklist");
    const selected = list.querySelector("li.selected");
    if (selected) {
        console.log("Selected Item: " + selected.innerText);
        queue.push(selected.innerText);
        console.log(queue);
        makePlaylist();
        setCurrentTrack();
        setNextTrack();
        addTrackToAudio();
    } else {
        console.log("Please select an item from the list.");
    }
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
    tracks_collection.forEach(track => {
        const li = document.createElement("li");
        li.innerHTML = track;
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
    console.log(document.getElementById("select"));
    document.getElementById("select").addEventListener("click", selectItem);
    makeTracklist();
}