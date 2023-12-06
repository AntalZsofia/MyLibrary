import React, { useState, useEffect } from 'react'
import './Profile';
import { useNavigate } from 'react-router';
import useAuth from '../../Hooks/useAuth';
import { isValidUsername, isValidEmail } from '../../Utility/Validation';
import { ThemeContext } from '../../Context/ThemeProvider';
import { useContext } from 'react';

export default function UpdateProfile() {

    const [username, setUsername] = useState('');
    const [newUsername, setNewUsername] = useState(null);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [newEmail, setNewEmail] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [isUpdateSuccessful, setIsUpdateSuccessful] = useState(false);
    const navigate = useNavigate();
    const { setUser } = useAuth();
    const [error, setError] = useState("");
    const { darkMode } = useContext(ThemeContext);

useEffect(() => {
    fetch('https://localhost:7276/api/user/me', {credentials: 'include'})
.then((res) => res.json())
      .then((data) => {
        console.log(data);
        setIsLoading(false);
        setUsername(data.username);
        setEmail(data.email);
        setPassword(data.password);
      })
      .catch((err) => console.error("Error fetching user profile", err));
  }, []);

  const handleUpdateProfile = async () => {

    try {
      if(!isValidUsername(newUsername)){
        setError("Username has to be at least 4 characters long")
        return
      };
      if(!isValidEmail(newEmail)){
        setError("Please enter a valid email address")
        return
      }
        const userData = {
            username: newUsername ? newUsername : null,
            email: newEmail ? newEmail : null,
            
        }
        const response = await fetch('https://localhost:7276/api/user/me', {
            method: 'PUT',
            credentials: 'include',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userData),
        });

        if (response.ok) {
            console.log('Profile updated successfully');
            setIsUpdateSuccessful(true);
            setUser(null);
            navigate('/login');
            
        } else {
            console.error('Profile update failed');
        }
    } catch (err) {
        console.error('Error updating user profile', err);
    }
};

      const handleCancel = () => {
        navigate('/profile');
      }
      if(isLoading){
        return <div>Loading...</div>;
      }
  return (
   <div className={`update-profile-container ${darkMode ? 'dark-mode' : ''}`}>
    <h2 className={`update-profile-header ${darkMode ? 'dark-mode' : ''}`}>Update Profile</h2>
    <h5>After changing your profile details, you need to log in again</h5>
    <div>
       <label className={`update-profile-label ${darkMode ? 'dark-mode' : ''}`} htmlFor="username"></label>
       <input
       className={`update-profile-input ${darkMode ? 'dark-mode' : ''}`}
         type="text"
         id="username"
         placeholder='Username'
         value={newUsername}
         onChange={(e) => setNewUsername(e.target.value)}
       />
     </div>
     <div>
       <label className="update-profile-label" htmlFor="email"></label>
       <input
       className={`update-profile-input ${darkMode ? 'dark-mode' : ''}`}
         type="text"
         id="email"
         placeholder='Email'
         value={newEmail}
         onChange={(e) => setNewEmail(e.target.value)}
       />
     </div>
     <div className='update-profile-button-container'>
     <button className={`update-profile-button ${darkMode ? 'dark-mode' : ''}`} onClick={handleUpdateProfile}>Update Profile</button>
     <button className={`update-profile-button ${darkMode ? 'dark-mode' : ''}`} onClick={handleCancel}>Cancel</button>
     {isUpdateSuccessful ? <p className="success-message">Profile update successful</p> : null}
        {error ? <p className='error-message'>{error}</p> : null}
     </div>
   </div>
  )
  }
