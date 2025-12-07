# Automated Test Scenarios for Online Shop

This document outlines the core automated test scenarios implemented for the Waracle Online Shop QA Challenge.

## User Accounts Covered
- `standard_user`: Normal user, site should work as expected
- `locked_out_user`: Should not be able to log in
- `problem_user`: Product images do not load
- `performance_glitch_user`: Experiences high loading times

## Test Scenarios

#1. Login Functionality
- **Valid login**: User can log in with valid credentials (`standard_user`).
- **Locked out user**: Attempting to log in as `locked_out_user` displays an error message.
- **Problem user**: Login succeeds, but product images are checked for default image icon.
- **Performance glitch user**: Login succeeds, and performance/timing checks are performed.

#2. Product Browsing
- **View product list**: User can see a list of products after login.
- **Sort products**: User can sort products by price (low to high) and verify sorting order.
- **Product details**: User can click a product to view its details and return to the product list.
- **Performance check**: Navigating to product details and back is measured for load time.

#3. Shopping Cart
- **Add to cart**: User can add a product to the cart and see the correct item count in the cart icon.
- **Remove from cart**: User can remove a product from the cart and verify the cart is empty.
- **Cart badge**: The cart icon displays the correct number of items after add/remove actions.

#4. Error Handling
- **Login error**: Invalid or locked out credentials display the correct error message.
- **Image loading error**: For `problem_user`, all product images are checked to ensure they display the default image icon.

#5. Performance
- **Sorting performance**: Sorting products completes within a specified time threshold.
- **Navigation performance**: Navigating to product details and back completes within a specified time threshold.

---
The tests can be run on mobile views as well as desktop browsers to ensure responsiveness and usability across devices.

These scenarios are automated using Selenium WebDriver and NUnit, following the Page Object Model for maintainability and clarity.
