<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DeualCalendar1.ascx.vb" Inherits="ERpMaen.DeualCalendar1" %>

<div class="w3-sidebar w3-bar-block w3-collapse w3-card w3-animate-left" style="width:220px;z-index:2" id="mySidebar">
    <style>
        .w3-bar-item input[type=radio],.w3-bar-item label{cursor:pointer}
    </style>
  <button class="w3-bar-item w3-button w3-ripple w3-large w3-hide-large" onclick="closeSidebar()">Close<span class="w3-right">&times;</span></button>
  <button class="w3-bar-item w3-button w3-ripple collapsed" onclick="dropdown(this)">
		<span>Set Language</span>
		<span class="w3-right"><svg width="10" height="10"><path d="M1 3L5 7L9 3Z"/></svg></span>
		<span class="w3-right"><svg width="10" height="10"><path d="M1 7L5 3L9 7Z"/></svg></span>
	</button>
		<div class="w3-bar-block w3-light-grey">
			<div class="w3-bar-item w3-button" onclick="selectLang(this)">
				<input id="langEn" class="w3-radio" type="radio" name="lang" value="en" checked>
				<label for="langEn">English</label>
			</div>
			<div class="w3-bar-item w3-button" onclick="selectLang(this)">
				<input id="langAr" class="w3-radio" type="radio" name="lang" value="ar">
				<label for="langAr">Arabic</label>
			</div>
			<div class="w3-bar-item w3-button" onclick="selectLang(this)">
				<input id="langId" class="w3-radio" type="radio" name="lang" value="id">
				<label for="langId">Indonesian</label>
			</div>
		</div>
  <button class="w3-bar-item w3-button w3-ripple collapsed" onclick="dropdown(this)">
		<span>Set Full Year</span>
		<span class="w3-right"><svg width="10" height="10"><path d="M1 3L5 7L9 3Z"/></svg></span>
		<span class="w3-right"><svg width="10" height="10"><path d="M1 7L5 3L9 7Z"/></svg></span>
	</button>
		<div class="w3-bar-block w3-light-grey">
			<div class="w3-bar-item">
				<input id="valYear" class="w3-input w3-border" type="text" placeholder="full year value...">
			</div>
			<button class="w3-bar-item w3-button w3-ripple" onclick="setYear()">Apply</button>
		</div>
  <button class="w3-bar-item w3-button w3-ripple collapsed" onclick="dropdown(this)">
		<span>Set Month</span>
		<span class="w3-right"><svg width="10" height="10"><path d="M1 3L5 7L9 3Z"/></svg></span>
		<span class="w3-right"><svg width="10" height="10"><path d="M1 7L5 3L9 7Z"/></svg></span>
	</button>
		<div class="w3-bar-block w3-light-grey">
			<div class="w3-bar-item">
				<input id="valMonth" class="w3-input w3-border" type="text" placeholder="month value...">
			</div>
			<button class="w3-bar-item w3-button w3-ripple" onclick="setMonth()">Apply</button>
		</div>
  <button class="w3-bar-item w3-button w3-ripple collapsed" onclick="dropdown(this)">
		<span>Set Theme</span>
		<span class="w3-right"><svg width="10" height="10"><path d="M1 3L5 7L9 3Z"/></svg></span>
		<span class="w3-right"><svg width="10" height="10"><path d="M1 7L5 3L9 7Z"/></svg></span>
	</button>
		<div class="w3-bar-block w3-light-grey">
			<div class="w3-bar-item">
				<input id="txtTheme" class="w3-input w3-border" type="text" placeholder="theme-color or index">
			</div>
			<button class="w3-bar-item w3-button w3-ripple" onclick="setTheme()">Apply</button>
		</div>
  <button class="w3-bar-item w3-button w3-ripple collapsed" onclick="dropdown(this)">
		<span>Set Today Timeout</span>
		<span class="w3-right"><svg width="10" height="10"><path d="M1 3L5 7L9 3Z"/></svg></span>
		<span class="w3-right"><svg width="10" height="10"><path d="M1 7L5 3L9 7Z"/></svg></span>
	</button>
		<div class="w3-bar-block w3-light-grey">
			<div class="w3-bar-item">
				<input id="valTimeout" class="w3-input w3-border" type="text" placeholder="timeout value...">
			</div>
			<button class="w3-bar-item w3-button w3-ripple" onclick="setTodayTimeout()">Apply</button>
		</div>
  <a class="w3-bar-item w3-button w3-ripple" href="https://codepen.io/zulns/full/adqQjq">See My Datepicker</a>
  <a class="w3-bar-item w3-button w3-ripple" href="https://codepen.io/zulns/full/BjejxN">See My Timepicker</a>
</div>

<div class="w3-main" style="margin-left:220px">
	<div class="w3-container w3-padding w3-teal">
		<button class="w3-button w3-ripple w3-xlarge w3-hide-large w3-left" style="margin:6px 8px 0px -16px" onclick="openSidebar()">
			<svg width="18" height="23"><path d="M0 6L18 6L18 8L0 8Z M0 13L18 13L18 15L0 15Z M0 20L18 20L18 22L0 22Z"/></svg>
		</button>
		<h2>Responsive Hijri/Gregorian Dual Calendar Demo</h2>
	</div>

	<div id="calendar" class="w3-container w3-margin-top"></div>
</div>

