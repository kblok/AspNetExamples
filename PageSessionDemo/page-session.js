function pageSessionCleanUp() {
    if (document.getElementById('IsBasePagePostBack').value !== '1') {
        PageMethods.CleanUpPageSession(document.getElementById('BasePagePageKey').value);
    }
}
