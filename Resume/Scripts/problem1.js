// Problem 1
function fndLg() {
    var p1Arr = document.getElementById('prob1').value;
    var lgArr = [];
    lgArr = p1Arr.split(",");

    var largest = 0;

    for (i = 0; i < lgArr.length; i++) {
        if (largest < parseInt(lgArr[i])) {
            largest = parseInt(lgArr[i]);
        }
    }
    document.getElementById('prob1Output').value = largest;
}