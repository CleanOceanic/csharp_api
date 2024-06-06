function toggleMenu() {
    const menuItemsContent = document.getElementsByClassName('menu-items')[0];

    if (menuItemsContent.classList.contains('menu-mobile-active')) {
        menuItemsContent.classList.remove('menu-mobile-active');
    } else {
        menuItemsContent.classList.add('menu-mobile-active');
    }

    const menuImg = document.getElementsByClassName('menu-hamburguer-img')[0];

    if (menuImg.classList.contains('menu-hamburguer-active')) {
        menuImg.classList.remove('menu-hamburguer-active');
    } else {
        menuImg.classList.add('menu-hamburguer-active');
    }
}

