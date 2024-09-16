let users = JSON.parse(localStorage.getItem('users')) || [];

function login() {
    const email = document.getElementById('loginEmail').value;
    const password = document.getElementById('loginPassword').value;
    const user = users.find(u => u.email === email && u.password === password);

    if (user) {
        localStorage.setItem('currentUser', JSON.stringify(user)); 
        window.location.href = "./opening/homepage.html"; 
    } else {
        document.getElementById('loginMessage').innerText = "You are not logged in, if you do not have a username, please register.";
    }
}

function signup() {
    const name = document.getElementById('signupName').value;
    const email = document.getElementById('signupEmail').value;
    const password = document.getElementById('signupPassword').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    const existingUser = users.find(u => u.email === email);

    if (existingUser) {
        document.getElementById('signupMessage').innerText = "This email already exists in the system. Please choose a different password";
        return;
    }

    if (password !== confirmPassword) {
        document.getElementById('signupMessage').innerText = "Passwords do not match. Please tap again.";
        return;
    }

    // הוסף את המשתמש החדש למערך
    const newUser = { name, email, password };
    users.push(newUser);
    
    // עדכן את localStorage עם המשתמשים החדשים
    localStorage.setItem('users', JSON.stringify(users));
    
    localStorage.setItem('currentUser', JSON.stringify(newUser)); // שמור את המשתמש הנוכחי
    window.location.href = "./opening/homepage.html"; // החלף לדף הבית שלך
}