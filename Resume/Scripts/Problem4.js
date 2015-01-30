// problem 4
function pal(text) {
    var arrText = text.split("").reverse().join("");
    // arrText.reverse();
    var test = text.split("").join("");

    arrText = arrText.toLowerCase();
    test = test.toLowerCase();
    var palTest = (arrText == test) ? "True" : "False";

    document.getElementById('prob4Output').value = palTest;

}