// problem 3
function fact(numFact) {
    var prodTot = numFact;

    for (i = numFact - 1; i > 1; i--) {
        prodTot = prodTot * i;

    }

    document.getElementById('prob3Output').value = prodTot;
}