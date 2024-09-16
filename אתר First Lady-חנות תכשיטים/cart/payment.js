document.addEventListener('DOMContentLoaded', function() {
    const form = document.querySelector('form');
    const paymentModal = document.getElementById('paymentModal');

    // פונקציה לנקות את הטופס
    function clearForm() {
        form.reset(); // זה ינקה את כל השדות בטופס
    }

    // פונקציה לשמור את פרטי התשלום
    function savePaymentDetails() {
        const paymentDetails = {
            cardNumber: form.cardNumber.value,
            expirationDate: form.expirationDate.value,
            cvv: form.cvv.value
        };
        localStorage.setItem('savedPaymentDetails', JSON.stringify(paymentDetails)); // שמירה ב-localStorage
    }


    // פונקציה לבדוק אם יש פרטי תשלום שמורים
    function loadSavedPaymentDetails() {
        const savedDetails = localStorage.getItem('savedPaymentDetails');
        if (savedDetails) {
            const details = JSON.parse(savedDetails);
            form.cardNumber.value = details.cardNumber;
            form.expirationDate.value = details.expirationDate;
            form.cvv.value = details.cvv;
        }
    }

    // טען את פרטי התשלום השמורים כאשר הדף נטען
    loadSavedPaymentDetails();

    // כאשר טופס נשלח
    form.addEventListener('submit', function(event) {
        event.preventDefault(); // למנוע שליחה מידית של הטופס
        document.getElementById('loadingOverlay').style.display = 'flex'; // להציג את הריבוע


        setTimeout(function() {
            // שינוי ההודעה בריבוע
            document.getElementById('loadingTitle').innerText = 'The transaction was successfully completed! ' // עדכון ההודעה
            document.getElementById('loadingTitle2').innerText ='Thanks for using our site'
            document.getElementById('loadingTitle3').innerText ='...was sent to your Email.'
            // להסתיר את הריבוע לאחר כמה שניות או לאחר שהמשתמש ילחץ על כפתור
            setTimeout(function() {
                paymentModal.style.display = 'none'; // להסתיר את הריבוע לאחר 3 שניות
                clearForm(); // לנקות את הטופס
                form.submit(); // לשלוח את הטופס
            }, 6000); //
        }, 3000); // 
    });

    // הוספת מאזין לאירוע לכפתור הסגירה
    document.getElementById('closePaymentModal').addEventListener('click', function() {
        paymentModal.style.display = 'none'; // להסתיר את המודל
        clearForm(); // לנקות את הטופס
    });
});

//פונקציה לשמירת פרטי התשלום
function savePaymentDetails() {
    if (document.getElementById('savePaymentDetails').checked) {
        const paymentDetails = {
            cardNumber: form.cardNumber.value,
            expirationDate: form.expirationDate.value,
            cvv: form.cvv.value
        };
        localStorage.setItem('savedPaymentDetails', JSON.stringify(paymentDetails)); // שמירה ב-localStorage
    }
}