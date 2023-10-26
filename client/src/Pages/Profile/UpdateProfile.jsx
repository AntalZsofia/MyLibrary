import React, { useState, useEffect } from 'react'
import './Profile';
import { useNavigate } from 'react-router';
import useAuth from '../../Hooks/useAuth';

export default function UpdateProfile() {

    const [username, setUsername] = useState('');
    const [newUsername, setNewUsername] = useState(null);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [newEmail, setNewEmail] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const navigate = useNavigate();
    const { setUser } = useAuth();

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
   <div className='update-profile-container'>
    <h2 className='update-profile-header'>Update Profile</h2>
    <h5>After changing your profile details, you need to log in again</h5>
    <div>
       <label className="update-profile-label" htmlFor="username">Username:</label>
       <input
       className="update-profile-input"
         type="text"
         id="username"
         value={newUsername}
         onChange={(e) => setNewUsername(e.target.value)}
       />
     </div>
     <div>
       <label className="update-profile-label" htmlFor="email">Email:</label>
       <input
       className="update-profile-input"
         type="text"
         id="email"
         value={newEmail}
         onChange={(e) => setNewEmail(e.target.value)}
       />
     </div>
     <div className='update-profile-button-container'>
     <button className="update-profile-button" onClick={handleUpdateProfile}>Update Profile</button>
     <button className="update-profile-button" onClick={handleCancel}>Cancel</button>
     </div>
   </div>
  )
  }
