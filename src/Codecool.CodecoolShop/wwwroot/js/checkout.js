const cartHead = document.querySelector("#cart__head");
const cartBody = document.querySelector("#cart__body");
const cartFooter = document.querySelector("#cart__footer");
const headerCartButton = document.querySelector("#header-shopping-cart");
let totalItems = document.createElement("span");
let totalPrice = document.createElement("span");
let Items = 0;
let Price = 0;

InIt()

function InIt() {
    GetShoppingCart();
}

async function GetShoppingCart() {
    let response = await ApiGet('/api/cart/GetAll');
    PopulateCartBody(response);
}

function PopulateCartBody(response) {
    cartBody.innerHTML = " ";
    cartHead.innerHTML = "Shopping cart";
    for (let item of response) {
        let bodyContent = document.createElement("div");
        bodyContent.classList.add("cart__body-content");
        let card = document.createElement("div");
        card.classList.add("cart__card");
        let imageBox = document.createElement("div");
        imageBox.classList.add("cart__card-image-box");
        let image = document.createElement("img");
        image.classList.add("cart__card-image");
        let details = document.createElement("div");
        details.classList.add("cart__card-details");
        let name = document.createElement("span");
        name.classList.add("cart__card-name");
        let counting = document.createElement("div");
        counting.classList.add("cart__card-counting");
        let countContainer = document.createElement("div");
        countContainer.classList.add("cart__card-count-container");
        let price = document.createElement("span");
        price.classList.add("cart__card-price");
        let quantity = document.createElement("span");
        quantity.classList.add("cart__card-quantity");
        let total = document.createElement("span");
        total.classList.add("cart__card-total");
        let deleteBtn = document.createElement("a");

        deleteBtn.innerHTML = "DELETE";
        deleteBtn.classList.add("delete-btn");
        deleteBtn.dataset.productId = item.product.id;
        deleteBtn.addEventListener('click', async event => {
            let id = event.currentTarget.dataset.productId;
            let response = await ApiGet(`/api/cart/Delete/${id}`);
            Items = 0;
            Price = 0;
            PopulateCartBody(response);
        });

        image.src = `../img/${item.product.image}`;
        name.innerHTML = item.product.name;
        price.innerHTML = `Price: ${item.product.defaultPrice}`;
        quantity.innerHTML = `Qty: ${item.quantity}`;
        total.innerHTML = `Total: ${item.quantity * item.product.defaultPrice}`;

        countContainer.appendChild(price);
        countContainer.appendChild(quantity);
        countContainer.appendChild(deleteBtn);
        counting.appendChild(countContainer);
        counting.appendChild(total);
        details.appendChild(name);
        details.appendChild(counting);
        imageBox.appendChild(image);
        card.appendChild(imageBox);
        card.appendChild(details);
        bodyContent.appendChild(card);
        cartBody.appendChild(bodyContent);

        Items += item.quantity;
        Price += item.quantity * item.product.defaultPrice;
    }

    totalItems.innerHTML = `Total items: ${Items}`;
    totalPrice.innerHTML = `Total price: ${Price}`;

    cartFooter.appendChild(totalItems);
    cartFooter.appendChild(totalPrice);

    headerCartButton.dataset.itemCount = Items;

    if (Items > 0) {
        headerCartButton.classList.add("notification");
    } else {
        headerCartButton.classList.remove("notification");
    }
}

async function ApiGet(url) {
    let response = await fetch(url);
    if (response.ok) {
        return await response.json()
    }
}