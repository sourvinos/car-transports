var accentMap = {
    "o": "ö",
    "ü": "u",
    "ä": "a",
    "ë": "e"
};

var accentMapDE = { };

var accentFold = function (s) {
    if (!s) { return ""; }
    var ret = "";
    for (var i = 0; i < s.length; i++) {
        ret += accentMap[s.charAt(i)] || s.charAt(i);
    }
    return ret;
};

$(".ui.dropdown").dropdown({
    fullTextSearch: true,
    showOnFocus: false,
    customFilter: function (searchTerm, $choice, text) {
        query = accentFold(searchTerm.toLowerCase());
        text = accentFold(text.toLowerCase());
        if (text.indexOf(query) > -1) {
            return true;
        }
        return false;
    }
});