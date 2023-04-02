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
        this.applyStyling();
    }

    applyStyling() {
        const linkElem = document.createElement("link");
        linkElem.setAttribute("rel", "stylesheet");
        linkElem.setAttribute("href", "../css/usercard.css");
        this.shadow.appendChild(linkElem);
    }
    render() {
        this.shadow.innerHTML = `
           
            <div class="usercard">
                <img class="userpic" src="${this.url}">
                <p class="nickname">${this.name}</p>
            </div>
`
    }
}
customElements.define("user-card", Usercard);