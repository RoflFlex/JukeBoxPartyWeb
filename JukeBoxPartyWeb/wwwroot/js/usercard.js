class Usercard extends HTMLElement {
    name;
    url;

    constructor(name, url) {
        super();
        this.name = name;
        this.url = url;
        this.shadow = this.attachShadow({ mode: "open" });
        this.applyStyling();
    }
    connectedCallback() {
        this.render();
    }

    applyStyling() {
        const linkElem = document.createElement("link");
        linkElem.setAttribute("rel", "stylesheet");
        linkElem.setAttribute("href", "/css/usercard.css");
        this.shadow.appendChild(linkElem);
    }
    render() {
        this.shadow.innerHTML = `
            <style>
                p {
                    font-size:15px;
                    margin: 0;
                }
                img {
                    width: 40px;
                }
                div {
                    padding: 6px;
                    border-radius: 10px;
                    border-style: solid;
                    border-color: aqua;
                    border-width: 3px;
                    margin-top: 3px;
                    margin-left: 2px;
                    margin-right: 2px;
                    margin-bottom: 3px;
                }
            </style>
            <div class="usercard">
                <img class="userpic" src="${this.url}">
                <p class="nickname">${this.name}</p>
            </div>
`
    }
}
customElements.define("user-card", Usercard);