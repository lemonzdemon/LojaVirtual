function DecimalPriceChange(element) {
    console.log(element.value);
    if (!element.value) {
        element.value = '0.00';
    }
    else if (!element.value.includes('.')) {

        element.value += '.00';
    }
}

