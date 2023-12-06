import React, { useState } from 'react';
import useAuth from './../../Hooks/useAuth';
import { useNavigate } from 'react-router-dom';
import './Login.css';
import Account from './../../Icons/account.png';
import { ThemeContext } from '../../Context/ThemeProvider';
import { useContext } from 'react';

function Login() {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const { setUser } = useAuth();
  const navigate = useNavigate();
  const { darkMode } = useContext(ThemeContext);

  const handleLogin = () => {
    const userData = {
      userName,
      password
    }
    fetch('https://localhost:7276/api/login', {
      method: 'POST',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(userData),
    })
    .then(response => {
      if (!response.ok) {
        return response.json().then(data => {
          throw new Error(data.errorMessage);
        });
      }
      return response.json();
    })
      .then((data) => {
        setUser(data);
        console.log(data);
        navigate('/');
      })
      .catch((error) => {
        console.error("There was some error while logging in", error)})
      }
      // useEffect(() => {
      //   If the user is already logged in, redirect them to MyBooks
      //   if (userName && password) {
      //     navigate('/');
      //   }
      // }, [userName, password, navigate]);
  return (
    <div className={`login-container ${darkMode ? 'dark-mode' : ''}`}>
      <div className={`login-card ${darkMode ? 'dark-mode' : ''}`}>
        <div className={`icon-container ${darkMode ? 'dark-mode' : ''}`}>
          <img src={Account} alt="Account" className="account-icon" />
        </div>
        <h2>Login</h2>
        <input
          type="text"
          className={`login-card-input ${darkMode ? 'dark-mode' : ''}`}
          id='Username'
          placeholder="Username"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
        />
        <input
          type="password"
          className={`login-card-input ${darkMode ? 'dark-mode' : ''}`}
          id='Password'
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button className={`login-button ${darkMode ? 'dark-mode' : ''}`} onClick={handleLogin}>
          Log In
        </button>
        {userName && password && (
          <div className={`login-info ${darkMode ? 'dark-mode' : ''}`}>
            <p>Logged in as: {userName}</p>
            <p>Password: {password}</p>
          </div>
        )}
      </div>
    </div>
  );

        }
export default Login;

