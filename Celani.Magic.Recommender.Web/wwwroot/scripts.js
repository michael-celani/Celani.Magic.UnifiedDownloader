function testNode(targ) {
    return (targ && targ.nodeName === "A" && targ.href.startsWith('https://scryfall.com/card/'))
}

document.addEventListener("mouseover", function(e) {
    if (!testNode(e.target)) {
        return;
    }


    var url = e.target.href.split('/');
    var id = url.pop();
    var image = "https://api.scryfall.com/cards/" + id + "?format=image&version=png";
    e.target.style.setProperty("--link", "url(" + image + ")");
});

document.addEventListener("mousemove", function(e) {
    if (!testNode(e.target)) {
        return;
    }

    e.target.style.setProperty("--top", String(e.clientY) + "px");
    e.target.style.setProperty("--left", String(e.clientX)+ "px");
});