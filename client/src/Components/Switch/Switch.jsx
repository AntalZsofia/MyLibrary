import React from 'react'
import './Switch.css';
import lightModeIcon from '../../Icons/sun2.png';
import darkModeIcon from '../../Icons/moon.png';

export default function Switch({ isToggled, onToggle }) {
    return (
        <label className="switch">
            <img src={lightModeIcon} alt="Light mode icon" className={`mode-icon light-icon ${isToggled ? 'hidden' : ''}`} />
            <img src={darkModeIcon} alt="Dark mode icon" className={`mode-icon dark-icon ${isToggled ? '' : 'hidden'}`} />        <input type="checkbox" checked={isToggled} onChange={onToggle} />
            <input type="checkbox" checked={isToggled} onChange={onToggle} />
            <span className="slider round"></span>
        </label>
    )
}
