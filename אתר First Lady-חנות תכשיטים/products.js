//פונקציה להוספת הפריט לסל הקניות
function AddToCartButton(name, cost, img, quantity) {
    let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    
    // חפש אם המוצר כבר קיים בעגלה
    let existingItem = cartItems.find(item => item.name === name);
    
    if (existingItem) {
        // אם המוצר כבר קיים, עדכן את הכמות
        existingItem.quantity += parseInt(quantity);
    } else {
        // אם לא, הוסף מוצר חדש לערך בעגלת הקניות
        cartItems.push({name, cost, img, quantity: parseInt(quantity)});
    }

    localStorage.setItem('cartItems', JSON.stringify(cartItems));
}
