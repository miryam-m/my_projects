let users = JSON.parse(localStorage.getItem('users')) || []; // קרא את המשתמשים מ-localStorage

function login() {
    const email = document.getElementById('loginEmail').value;
    const password = document.getElementById('loginPassword').value;
    const user = users.find(u => u.email === email && u.password === password);

    if (user) {
        localStorage.setItem('currentUser', JSON.stringify(user)); // שמור את המשתמש הנוכחי
        window.location.href = "./opening/homepage.html"; // החלף לדף הבית שלך
    } else {
        document.getElementById('loginMessage').innerText = "אתה לא מחובר, אם אין לך שם משתמש, אנא הרשמו.";
    }
}

function signup() {
    const name = document.getElementById('signupName').value;
    const email = document.getElementById('signupEmail').value;
    const password = document.getElementById('signupPassword').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    const existingUser = users.find(u => u.email === email);

    if (existingUser) {
        document.getElementById('signupMessage').innerText = "אימייל זה כבר קיים במערכת. אנא בחר סיסמא אחרת.";
        return;
    }

    if (password !== confirmPassword) {
        document.getElementById('signupMessage').innerText = "סיסמאות לא תואמות. אנא הקש שוב.";
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