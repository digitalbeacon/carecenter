
if(typeof DigitalBeacon == 'undefined') DigitalBeacon = {};
if(!DigitalBeacon.CareCenter) DigitalBeacon.CareCenter = {};

DigitalBeacon.CareCenter.Utils = (function() {
    function Utils() {
    }
    return Utils;
})();
DigitalBeacon.CareCenter.Utils.age = function (dob) {
    var today = new Date();
    var age = today.getFullYear() - dob.getFullYear();
    var temp = new Date(today.getFullYear() - age, today.getMonth(), today.getDate());
    if (dob.getTime() > temp.getTime()) {
        age--;
    }
    return age;
};
DigitalBeacon.CareCenter.Utils.parseDecimal = function (val) {
    if (val === null) {
        return NaN;
    }
    return parseFloat(val.replace('$', '').replace(',', ''));
};
DigitalBeacon.CareCenter.Utils.formatDecimal = function (val) {
    return (Math.round(val * 100) % 100 === 0) ? val.toFixed(0) : val.toFixed(2);
};
