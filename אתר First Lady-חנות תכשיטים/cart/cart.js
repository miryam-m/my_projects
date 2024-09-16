//הצגת המוצר בסל הקניות
function displayCart() {
    const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    const container = document.getElementById('cart-container');
    container.innerHTML = '';
    let totalCost = 0;

    if (cartItems.length === 0) {
        container.innerHTML = '<p>Your shopping cart is empty.</p>';
      //  document.getElementById('summary').innerHTML = ''; // ריקון הסיכום
      document.getElementById('summary').innerHTML = `
    <h2>Summary of the purchase:</h2>
        <p>Quantity of products: 0$</p>
        <p>Total Price:0$</p>
    `;
        return;
    }

    cartItems.forEach((item, index) => {
        const itemTotalCost = item.cost * item.quantity; // חישוב עלות כוללת עבור פריט
        container.innerHTML += `
            <div class="cart-item">
                <img src="${item.img}" alt="${item.name}">
                <div class="product-details">
                    <p>${item.name}</p>
                    <p>Price per unit: ${item.cost}$</p>
                    <p>Total price: ${itemTotalCost}$</p>
                </div>
                <div class="product-details">
                <p>Number of item:</P>
                <input class="quantity-input" type="number" value="${item.quantity}" onchange="updateQuantity(${index}, this.value)">
                <button class="remove-button" onclick="removeFromCart(${index})">Deleting an item</button>
           </div>
                </div>
        `;
        totalCost += itemTotalCost; 
    });

    document.getElementById('summary').innerHTML = `
    <h2>Summary of the purchase:</h2>
        <p>Quantity of products: ${cartItems.length}</p>
        <p>Total Price: ${totalCost}$</p>
    `;
}

//עדכון כמות המוצרים
function updateQuantity(index, quantity) {
    const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    if (quantity < 1) quantity = 1; // לוודא שהכמות לא תהיה פחות מ-1
    cartItems[index].quantity = quantity;
    localStorage.setItem('cartItems', JSON.stringify(cartItems));
    displayCart();
}
//הסרת פריט מסל הקניות
function removeFromCart(index) {
    let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    cartItems.splice(index, 1);
    localStorage.setItem('cartItems', JSON.stringify(cartItems));
    displayCart();
}

//מחיקת כל הפריטים מסל הקניות
function clearCart() {
    localStorage.removeItem('cartItems');
    displayCart(); // עדכון המסך לאחר ריקון הסל
}

document.addEventListener('DOMContentLoaded', (event) => {
    displayCart();
});


document.getElementById('openPaymentModal').onclick = function() {
    document.getElementById('paymentModal').style.display = 'block';
}

document.getElementById('closePaymentModal').onclick = function() {
    document.getElementById('paymentModal').style.display = 'none';
}

// כדי לסגור את המודל אם לוחצים מחוץ לו
window.onclick = function(event) {
    var modal = document.getElementById('paymentModal');
    if (event.target == modal) {
        modal.style.display = 'none';
    }
}
