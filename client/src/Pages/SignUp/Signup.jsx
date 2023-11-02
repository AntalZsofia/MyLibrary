import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './SignUp.css';
import useAuth from './../../Hooks/useAuth';
import { isValidUsername, isValidPassword, isValidEmail } from '../../Utility/Validation';
import Account from './../../Icons/account.png';

function Registration() {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [email, setEmail] = useState('');
  const [isRegistrationSuccessful, setIsRegistrationSuccesful] = useState(false);
  const { setUser } = useAuth();
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleRegistration = () => {
    if(!isValidUsername(userName)){
      setError("Username has to be at least 4 characters long")
      return
    };
    if(!isValidEmail(email)){
      setError("Please enter a valid email address")
      return
    };
    if(!isValidPassword(password)){
      setError("Password must contain at least one capital letter, one number, and one special character")
      return
    };
    const userData = {
      userName,
      password,
      email,
    };
    fetch('https://localhost:7276/api/signup', {
      method: 'POST',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(userData),
    })
      .then((response) => {
        if (!response.ok) {
          return response.json().then((data) => {
            throw new Error(data.errorMessage);
          });
        }
        setIsRegistrationSuccesful(true);
        return response.json();
      })
      .then((data) => {
        setUser(data);
        console.log(data);
        navigate('/'); 
      })
      .catch((error) => {
        console.error('There was an error while registering', error);
      });
  };

  return (
    <div className="registration-container">
      <div className="registration-card">
      <div className="icon-container">
          <img src={Account} alt="Account" className="account-icon" />
        </div>
        <h2>Registration</h2>
        <input
          type="text"
          className="registration-input"
          placeholder="Username"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}

        />
        <input
          type="password"
          className="registration-input"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}

        />
        <input
          type="email"
          className="registration-input"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <button className="registration-button" onClick={handleRegistration}>
          Register
        </button>
        {isRegistrationSuccessful ? <p className="success-message">Registration successful</p> : null}
        {error ? <p className='error-message'>{error}</p> : null}

      </div>
    </div>
  );
}

export default Registration;