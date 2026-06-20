$(function () {

    // Counters
    $('.counter').countUp();


    // reveal items on scroll
    function reveal() {
        var reveals = document.querySelectorAll('.reveal');

        for (var i = 0; i < reveals.length; i++) {

            var windowHeight = window.innerHeight;
            var cardTop = reveals[i].getBoundingClientRect().top;
            var cardRevealPoint = 150;

            if (cardTop < windowHeight - cardRevealPoint) {
                reveals[i].classList.add('active');
            }
            else {
                reveals[i].classList.remove('active');
            }
        }
    }
    window.addEventListener('scroll', reveal);
    reveal();


    // LOADER
    $("#global-loader").fadeOut("slow");

    // PAGE LOADING
    $(window).on("load", function (e) {
        $("#global-loader").fadeOut("slow");
    })

    // BACK TO TOP BUTTON
    $(window).on("scroll", function (e) {
        if ($(this).scrollTop() > 130) {
            $('#back-to-top').removeClass('animate-reverse');
            $('#back-to-top').addClass('animate');
            $('#back-to-top').fadeIn('slow');
        } else {
            $('#back-to-top').removeClass('animate');
            $('#back-to-top').addClass('animate-reverse');
            $('#back-to-top').fadeOut('slow');
        }
    });

    // Landing Side Menu
    var landingSidemenu = $(".sidemenu-landing"),
        navHeader = $(".nav-header-main.sticky"),
        body = $("body"),
        navHeight;

    ///Create a margin top to prevent content 'jumps':
    landingSidemenu.before('<div class="prevent-top"></div>');
    function preventTop() {
        navHeight = navHeader.innerHeight();
        landingSidemenu.css({ "margin-top": + navHeight + "px" });
    };
    preventTop(); //Run.

    // Toggle Landing Page Sidemenu
    openLandingSidemenu = () => {
        $(body).toggleClass("landing-sidemenu-open");
    }

    // click outside of sidemenu to close
    $('.landing-page-content').on('click touchstart', function (event) {
        event.stopPropagation();

        // closing of sidebar menu when clicking outside of it
        if (!$(event.target).closest('#landingMenuBtn').length) {
            var sidebarTarg = $(event.target).closest('#landingMenuBtn').length;
            if (!sidebarTarg) {
                $('body').removeClass('landing-sidemenu-open');
            }
        }
    });

    // for menu scroll
    const pageLink = document.querySelectorAll(".nav-scroll");

    pageLink.forEach((elem) => {
        elem.addEventListener("click", (e) => {
            e.preventDefault();

            let WinUrl = window.location.pathname.split('/');
            if (WinUrl[WinUrl.length - 1] == 'landing.html') {
                window.location.assign(elem.getAttribute("href"))
            }

            let headerOffset = 55;
            let elemOffsetTop = document.querySelector(elem.getAttribute("href"))
            let elementPosition = elemOffsetTop.getBoundingClientRect().top - headerOffset;
            window.scrollBy(0, elementPosition);

        });
    });

    // section menu active
    function onScroll(event) {
        const sections = document.querySelectorAll(".nav-scroll");
        const scrollPos = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop;

        sections.forEach((ele, index) => {
            const val = ele.getAttribute("href");
            const refElement = document.querySelector(val);
            const scrollTopMinus = scrollPos + 95;
            if (
                refElement.offsetTop <= scrollTopMinus &&
                refElement.offsetTop + refElement.offsetHeight > scrollTopMinus
            ) {
                ele.classList.add("active");
            }
            else {
                ele.classList.remove("active");
            }
        })
    }
    window.document.addEventListener("scroll", onScroll);


});

// ----------------------------------
"use strict";
let mainContent;
(function () {
    let html = document.querySelector('html');
    mainContent = document.querySelector('.app-content');

    // theme color picker
    const dynamicPrimaryLight = document.querySelectorAll('input.color-primary-light');
    const dynamicBackground = document.querySelectorAll('input.color-bg-transparent');
    dynamicPrimaryColor(dynamicPrimaryLight);
    dynamicBackgroundColor(dynamicBackground);

    localStorageBackup();



    //RTL 
    if (!localStorage.getItem("spanerrtl")) {
        // html.setAttribute("dir" , "rtl") // for rtl version 
    }

    //Light Theme Style
    if (!localStorage.getItem("spanerlighttheme")) {
        // html.setAttribute("data-theme-color" , "light") // for light theme 
    }

    //Dark Theme Style
    if (!localStorage.getItem("spanerdarktheme")) {
        // html.setAttribute("data-theme-color" , "dark") // for dark theme 
    }

    /*RTL Start*/
    if (html.getAttribute('dir') === "rtl") {
        rtlFn();
    }
    /*RTL End*/

    /*Horizontal Start*/
    if (html.getAttribute('data-hor-style') === "hor-click") {
        horizontalClickFn();
    }
    /*Horizontal End*/

    if (document.querySelector(".sidebar-right1")) {
        switcherClick();
    }

    checkOptions();

})();

function switcherClick() {
    let ltrBtn, rtlBtn, lightBtn, darkBtn, resetBtn;
    let html = document.querySelector('html');
    lightBtn = document.querySelector('#switchbtn-light');
    darkBtn = document.querySelector('#switchbtn-dark');
    ltrBtn = document.querySelector('#switchbtn-ltr');
    rtlBtn = document.querySelector('#switchbtn-rtl');
    resetBtn = document.querySelector('#resetbtn');

    /*Light Layout Start*/
    let lightThemeVar = lightBtn.addEventListener('click', () => {
        html.setAttribute('data-theme-color', 'light');
        html.setAttribute('data-header-style', 'gradient');
        html.setAttribute('data-menu-style', 'dark');
        $('#switchbtn-lightmenu').prop('checked', true);
        $('#switchbtn-lightheader').prop('checked', true);

        localStorage.setItem("spanerlighttheme", true);
        localStorage.removeItem("spanerdarktheme");
        localStorage.removeItem("spanerbgColor");
        localStorage.removeItem("spanerheaderbg");
        localStorage.removeItem("spanerbgwhite");
        localStorage.removeItem("spanermenubg");

        localStorage.setItem("spanerHeader", 'gradient');
        localStorage.setItem("spanerMenu", 'dark');

    })
    /*Light Layout End*/

    /*Dark Layout Start*/
    let darkThemeVar = darkBtn.addEventListener('click', () => {
        html.setAttribute('data-theme-color', 'dark');
        html.setAttribute('data-header-style', 'dark');
        html.setAttribute('data-menu-style', 'dark');
        $('#switchbtn-darkmenu').prop('checked', true);
        $('#switchbtn-darkheader').prop('checked', true);

        localStorage.setItem("spanerdarktheme", true);
        localStorage.removeItem("spanerlighttheme");
        //


        localStorage.setItem("spanerHeader", 'dark');
        localStorage.setItem("spanerMenu", 'dark');

        checkOptions();


    });
    /*Dark Layout End*/
    /* rtl start*/
    let rtlVar = rtlBtn?.addEventListener('click', () => {
        localStorage.setItem("spanerrtl", true);
        localStorage.removeItem("spanerltr");
        rtlFn();
    });
    /* rtl end*/
    /* ltr start*/
    let ltrVar = ltrBtn?.addEventListener('click', () => {
        //    local storage 
        localStorage.setItem("spanerltr", true);
        localStorage.removeItem("spanerrtl");

        ltrFn();
    });
    /* ltr end*/

}

function ltrFn() {
    let html = document.querySelector('html');
    html.setAttribute("dir", "ltr");
    let select2Cont = document.querySelectorAll(".select2-container")
    select2Cont.forEach(e => e.setAttribute("dir", "ltr"))
    document.querySelector("#style")?.setAttribute("href", "../assets/plugins/bootstrap/css/bootstrap.min.css");
    var carousel = $('.owl-carousel');
    $.each(carousel, function (index, element) {
        // element == this
        var carouselData = $(element).data('owl.carousel');
        carouselData.settings.rtl = false; //don't know if both are necessary
        carouselData.options.rtl = false;
        $(element).trigger('refresh.owl.carousel');
    });
    if (html.getAttribute('data-layout') === "horizontal") {
        checkHoriMenu();
    }
    //
    checkOptions();
}

function rtlFn() {
    let html = document.querySelector('html');
    html.setAttribute("dir", "rtl");
    let select2Cont = document.querySelectorAll(".select2-container")
    select2Cont.forEach(e => e.setAttribute("dir", "rtl"))
    document.querySelector("#style")?.setAttribute("href", "../assets/plugins/bootstrap/css/bootstrap.rtl.min.css");
    var carousel = $('.owl-carousel');
    $.each(carousel, function (index, element) {
        // element == this
        var carouselData = $(element).data('owl.carousel');
        carouselData.settings.rtl = true; //don't know if both are necessary
        carouselData.options.rtl = true;
        $(element).trigger('refresh.owl.carousel');
    });
    if (html.getAttribute('data-layout') === "horizontal") {
        checkHoriMenu();
    }
    //
    checkOptions();
}


function horizontalClickFn() {
    $('#switchbtn-horizontal').prop('checked', true);
    let html = document.querySelector('html');
    html.setAttribute('data-layout', 'horizontal');
    html.setAttribute('data-hor-style', 'hor-click');
    html.removeAttribute('data-vertical-style');
    if (!document.body.classList.contains('auth-page')) {

        ActiveSubmenu();
        document.querySelector(".main-content").classList.add("horizontal-content");
        let mainContainer = document.querySelectorAll(".main-container")
        mainContainer.forEach(e => e.classList.add("container"))
        mainContainer.forEach(e => e.classList.remove("container-fluid"))
        document.querySelector(".app-header").classList.add("hor-header");
        document.querySelector(".app-sidebar").classList.add("horizontal-main");
        document.querySelector(".main-sidemenu").classList.add("container");

        document.querySelector(".main-content").classList.remove("app-content");
        document.body.classList.remove('sidebar-mini');
        document.body.classList.remove('sidenav-toggled');
        responsive();
        menuClick();
        mainContent.addEventListener('click', slideClick);
        setTimeout(() => {
            if (window.innerWidth >= 992) {
                slideClick()
            }
            checkHoriMenu();
        }, 800)
        //
        checkOptions();
    }
}

function resetData() {
    let html = document.querySelector('html');
    $('#switchbtn-ltr').prop('checked', true);
    $('#switchbtn-light').prop('checked', true);
    $('#switchbtn-lightmenu').prop('checked', true);
    $('#switchbtn-lightheader').prop('checked', true);
    html.setAttribute('data-theme-color', 'light');
    html.setAttribute('data-header-style', 'gradient');
    html.setAttribute('data-menu-style', 'dark');
    html.setAttribute('data-vertical-style', 'default');
    document.body.classList.remove('sidenav-toggled');
    ltrFn();
    localStorage.clear();
}

function checkOptions() {

    let html = document.querySelector('html')

    // dark
    if (html.getAttribute('data-theme-color') === "dark") {
        $('#switchbtn-dark').prop('checked', true);
    }


    // RTL 
    if (html.getAttribute('dir') === "rtl") {
        $('#switchbtn-rtl').prop('checked', true);
    }

}

const handleThemeUpdate = (cssVars) => {
    const root = document.querySelector(':root');
    const keys = Object.keys(cssVars);
    keys.forEach(key => {
        root.style.setProperty(key, cssVars[key]);
    });
}

// to check the value is hexa or not
const isValidHex = (hexValue) => /^#([A-Fa-f0-9]{3,4}){1,2}$/.test(hexValue)

const getChunksFromString = (st, chunkSize) => st.match(new RegExp(`.{${chunkSize}}`, "g"))
// convert hex value to 256
const convertHexUnitTo256 = (hexStr) => parseInt(hexStr.repeat(2 / hexStr.length), 16)
// get alpha value is equla to 1 if there was no value is asigned to alpha in function
const getAlphafloat = (a, alpha) => {
    if (typeof a !== "undefined") { return a / 255 }
    if ((typeof alpha != "number") || alpha < 0 || alpha > 1) {
        return 1
    }
    return alpha
}
// convertion of hex code to rgba code
function hexToRgba(hexValue, alpha) {
    if (!isValidHex(hexValue)) { return null }
    const chunkSize = Math.floor((hexValue.length - 1) / 3)
    const hexArr = getChunksFromString(hexValue.slice(1), chunkSize)
    const [r, g, b, a] = hexArr.map(convertHexUnitTo256)
    return `rgba(${r}, ${g}, ${b}, ${getAlphafloat(a, alpha)})`
}

// convertion of hex code to rgb code
function hexToRgb(hexValue) {
    if (!isValidHex(hexValue)) { return null }
    const chunkSize = Math.floor((hexValue.length - 1) / 3)
    const hexArr = getChunksFromString(hexValue.slice(1), chunkSize)
    const [r, g, b] = hexArr.map(convertHexUnitTo256)
    return `${r}, ${g}, ${b}`
}

function themeSwitch(switchProperty) {
    switchProperty.forEach((item) => {
        item.addEventListener('click', (e) => {
            const primaryColor = e.target.getAttribute('data-bg-color')
            const primaryHoverColor = e.target.getAttribute('data-bg-hover')
            const primaryBorderColor = e.target.getAttribute('data-bg-border')
            const primaryTransparent = e.target.getAttribute('data-bg-transparent')

            handleThemeUpdate({
                '--primary-bg-color': primaryColor,
                '--primary-bg-hover': primaryHoverColor,
                '--primary-bg-border': primaryBorderColor,
                '--primary-transparentcolor': primaryTransparent,
            });

            $("input.input-color-picker[data-id='bg-color']").val(primaryColor)
            $("input.input-color-picker[data-id1='bg-hover']").val(primaryColor)
            $("input.input-color-picker[data-id2='bg-border']").val(primaryColor)
            $("input.input-color-picker[data-id3='transparentcolor']").val(primaryColor)
        });
    });
}

function dynamicPrimaryColor(primaryColor) {
    primaryColor.forEach((item) => {
        item.addEventListener('input', (e) => {
            const cssPropName = `--primary-${e.target.getAttribute('data-id')}`;
            const cssPropName1 = `--primary-${e.target.getAttribute('data-id1')}`;
            const cssPropName2 = `--primary-${e.target.getAttribute('data-id2')}`;
            const cssPropName3 = `--primary-${e.target.getAttribute('data-id3')}`;
            handleThemeUpdate({
                [cssPropName]: hexToRgba(e.target.value),
                // 95 is used as the opacity 0.95
                [cssPropName1]: hexToRgba(e.target.value, 0.99),
                [cssPropName2]: hexToRgba(e.target.value, 0.2),
                [cssPropName3]: hexToRgba(e.target.value, 0.2),
            });
        });
    });
}
function dynamicBackgroundColor(bgColor) {
    bgColor.forEach((item) => {
        item.addEventListener('input', (e) => {
            const cssPropName5 = `--${e.target.getAttribute('data-id5')}`;
            const cssPropName6 = `--${e.target.getAttribute('data-id6')}`;
            const cssPropName7 = `--${e.target.getAttribute('data-id7')}`;
            const cssPropName8 = `--${e.target.getAttribute('data-id8')}`;
            handleThemeUpdate({
                [cssPropName5]: hexToRgba(e.target.value, 0.9),
                // 95 is used as the opacity 0.95
                [cssPropName6]: e.target.value,
                [cssPropName7]: e.target.value,
                [cssPropName8]: e.target.value,
            });
            if (!document.body.classList.contains('auth-page')) {
                let mainHeader = document.querySelector('.app-header');
                mainHeader.setAttribute('style', `--header-bg: ${e.target.value}`)
                let appSidebar = document.querySelector('.app-sidebar');
                appSidebar.setAttribute('style', `--menu-bg: ${e.target.value}`)
            }
        });
    });

}



function changePrimaryColor() {

    let userColor = document.getElementById('colorID').value;
    localStorage.setItem('spanerprimaryColor', userColor);
    // to store value as opacity 0.95 we use 95
    localStorage.setItem('spanerprimaryHoverColor', userColor + 99);
    localStorage.setItem('spanerprimaryBorderColor', userColor + 20);
    localStorage.setItem('spanerprimaryTransparent', userColor + 20);
    names()
}

function transparentBgColor() {
    let userColor1 = document.getElementById('transparentBgColorID').value;
    localStorage.setItem('spanerbgColor', userColor1 + "e6");
    localStorage.setItem('spanerbgwhite', userColor1);
    localStorage.setItem('spanermenubg', userColor1);
    localStorage.setItem('spanerheaderbg', userColor1);
    names()
    let html = document.querySelector('html');
    html.setAttribute('data-theme-color', 'dark');
    html.setAttribute('data-menu-style', 'dark');
    html.setAttribute('data-header-style', 'dark');
    $('#switchbtn-dark').prop('checked', true);
}
function localStorageBackup() {
    // if there is a value stored, update color picker and background color
    // Used to retrive the data from local storage
    if (localStorage.spanerprimaryColor) {
        if (document.getElementById('colorID')) {
            document.getElementById('colorID').value = localStorage.spanerprimaryColor;
        }
        document.querySelector('html').style.setProperty('--primary-bg-color', localStorage.spanerprimaryColor);
        document.querySelector('html').style.setProperty('--primary-bg-hover', localStorage.spanerprimaryHoverColor);
        document.querySelector('html').style.setProperty('--primary-bg-border', localStorage.spanerprimaryBorderColor);
        document.querySelector('html').style.setProperty('--primary-transparentcolor', localStorage.spanerprimaryTransparent);
        document.querySelector('html').style.setProperty('--primary-bg-color-rgb', localStorage.spanerprimaryRGB);
    }
    if (localStorage.spanerbgColor) {
        if (document.getElementById('transparentBgColorID')) {
            document.getElementById('transparentBgColorID').value = localStorage.spanerbgColor;
        }
        document.querySelector('html').style.setProperty('--background', localStorage.spanerbgColor);
        document.querySelector('html').style.setProperty('--white', localStorage.spanerbgwhite);
        document.querySelector('html').style.setProperty('--menu-bg', localStorage.spanermenubg);
        document.querySelector('html').style.setProperty('--header-bg', localStorage.spanerheaderbg);
        let html = document.querySelector('html');
        html.setAttribute('data-theme-color', 'dark');
        $('#switchbtn-dark').prop('checked', true);
        $('#switchbtn-darkmenu').prop('checked', true);
        $('#switchbtn-darkheader').prop('checked', true);

        localStorage.removeItem("spanerHeader", 'dark');
        localStorage.removeItem("spanerHeader", 'light');
    }
    if (localStorage.spanerdarktheme) {
        let html = document.querySelector('html');
        html.setAttribute('data-theme-color', 'dark');
    }
    if (localStorage.spanerrtl) {
        let html = document.querySelector('html');
        html.setAttribute('dir', 'rtl');
    }

}
// ______________ Cover-image
$(".cover-image").each(function () {
    var attr = $(this).attr('data-bs-image-src');
    if (typeof attr !== typeof undefined && attr !== false) {
        $(this).css('background', 'url(' + attr + ') center center');
    }
});

// Swticher Cookie Base
jQuery('.switcher-icon').click(function(){
	if(jQuery('.demo_changer').hasClass("active")){
		jQuery('.demo_changer').animate({"inset-inline-end":"-270px"},function(){
			jQuery('.demo_changer').removeClass("active");
		});
	}else{
		jQuery('.demo_changer').animate({"inset-inline-end":"0px"},function(){
			jQuery('.demo_changer').addClass("active");
		});
	} 
});
  
jQuery('.page').click(function(){
	if(jQuery('.demo_changer').hasClass("active")){
		jQuery('.demo_changer').animate({"inset-inline-end":"-270px"},function(){
			jQuery('.demo_changer').removeClass("active");
		});
	}
	
});

