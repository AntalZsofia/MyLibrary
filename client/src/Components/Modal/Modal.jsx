import React from 'react';
import './Modal.css';
import { ThemeContext } from '../../Context/ThemeProvider';
import { useContext } from 'react';

const Modal = ({ children, onClose }) => {
  const { darkMode } = useContext(ThemeContext);
  return (
    <div className={`modal-overlay ${darkMode ? 'dark-mode' : ''}`}>
      <div className={`modal ${darkMode ? 'dark-mode' : ''}`}>
        <button className="close-modal" onClick={onClose}>
          &times;
        </button>
        {children}
      </div>
    </div>
  );
};

export default Modal;