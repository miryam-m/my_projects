let currMoleTile;
let currPlantTile;
let moleInterval;
let plantInterval;
let score = 0;
let gameRunning = false; // מצב המשחק

window.onload = function () {
    setGame();
}

function setGame() {
    for (let i = 0; i < 9; i++) {
        let tile = document.createElement("div");
        tile.id = i.toString();
        tile.addEventListener("click", selectTile);
        document.getElementById("board").appendChild(tile);
    }
}

function startGame(initialScore = 0) {

    document.getElementById('loadingOverlay').style.display = 'none';
    if (!gameRunning) {
        
        score = initialScore;
        document.getElementById("score").innerText = score.toString();
        gameRunning = true;

        moleInterval = setInterval(setMole, 1000);
        plantInterval = setInterval(setPlant, 2000);
    }
}

function resetGame() {
    score = 0; // איפוס הניקוד
    document.getElementById("score").innerText = score.toString();
    if (currMoleTile) currMoleTile.innerHTML = "";
    if (currPlantTile) currPlantTile.innerHTML = "";

    clearInterval(moleInterval); // מנקה את ה-interval של המול
    clearInterval(plantInterval); // מנקה את ה-interval של הצמח

    gameRunning = false; // איפוס מצב המשחק
}

function stopContinueGame() {
    if (gameRunning) {
        // אם המשחק רץ, נעצור אותו
        gameRunning = false; // עצירת המשחק
        clearInterval(moleInterval); // מנקה את ה-interval של המול
        clearInterval(plantInterval); // מנקה את ה-interval של הצמח
    } else {
        // אם המשחק לא רץ, נתחיל אותו מחדש
        startGame(score ); 
    }
}

function getRandomTile() {
    let num = Math.floor(Math.random() * 9);
    return num.toString();
}

function setMole() {
    if (!gameRunning) return;

    if (currMoleTile) {
        currMoleTile.innerHTML = "";
    }

    let mole = document.createElement("img");
    mole.src = "./img/monty-mole.png";

    let num = getRandomTile();
    if (currPlantTile && currPlantTile.id == num) {
        return;
    }
    currMoleTile = document.getElementById(num);
    currMoleTile.appendChild(mole);
}

function setPlant() {
    if (!gameRunning) return;

    if (currPlantTile) {
        currPlantTile.innerHTML = "";
    }

    let plant = document.createElement("img");
    plant.src = "./img/piranha-plant.png";

    let num = getRandomTile();
    if (currMoleTile && currMoleTile.id == num) {
        return;
    }
    currPlantTile = document.getElementById(num);
    currPlantTile.appendChild(plant);
}

function selectTile() {
    if (!gameRunning) return;

    if (this == currMoleTile) {
        score += 10;
        document.getElementById("score").innerText = score.toString();
    } else if (this == currPlantTile) {
        stopContinueGame(); // עצירת המשחק במקרה של צמח
        document.getElementById('loadingOverlay').style.display = 'flex'; // להציג את הריבוע
        document.getElementById('loadingTitle3').innerText ='Yout score is: '+score;
    }
}