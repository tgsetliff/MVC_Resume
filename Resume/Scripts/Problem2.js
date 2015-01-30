// Problem 2
function sumMult(p2Arr) {
    var lgArr = [];
    lgArr = p2Arr.split(",");

    sumTot = sum(lgArr);
    multTot = mult(lgArr);

    document.getElementById('prob2Output').value = sumTot + "," + multTot;
}


function mult(numArray) {
    var multTot = 0;
    for (var i = 0; i < numArray.length; i++) {
        if (i === 0) {
            multTot = parseInt(numArray[i]);
        }
        else {
            multTot = multTot * parseInt(numArray[i]);
        }
    }

    return multTot;
}