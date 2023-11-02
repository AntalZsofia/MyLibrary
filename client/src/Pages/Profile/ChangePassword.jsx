import React, { useState } from 'react'
import './Profile.css';
import { useNavigate } from 'react-router';
import useAuth from '../../Hooks/useAuth';
import { isValidPassword } from '../../Utility/Validation';

export default function ChangePassword() {
    const [oldPassword, setOldPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const navigate = useNavigate();
    const { setUser } = useAuth();
    const [error, setError] = useState("");
    const[isPasswordChangeSuccessful, setIsPasswordChangeSuccessful] = useState(false);

const handleChangePassword = async () => {
    try{
      if(!isValidPassword(newPassword)){
        setError("Password must contain at least one capital letter, one number, and one special character")
        return
      };
        const userData = {
            oldPassword,
            newPassword
        }
        const response = await fetch('https://localhost:7276/api/user/me', {
            method: 'PUT',
            credentials: 'include',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userData),
        });
        if(response.ok){
            console.log('Password updated successfully');
            setIsPasswordChangeSuccessful(true);
            setUser(null);
            navigate('/login');
        }
        else{
            console.error('Password update failed');
            navigate('/profile');
        }
    }catch (err){
console.error('Error updating password', err);
    }
}
const handleCancel = ()=> {
    navigate('/profile');
}

  return (
    <div>
      <div className='change-password-container'>
      <h2>Change Password</h2>
      <h5>After changing your password, you need to log in again</h5>
      <input
      className='update-profile-input'
        type="password"
        placeholder="Old Password"
        value={oldPassword}
        onChange={(e) => setOldPassword(e.target.value)}
      />
      <input
      className='update-profile-input'
        type="password"
        placeholder="New Password"
        value={newPassword}
        onChange={(e) => setNewPassword(e.target.value)}
      />
      <button className='profile-button' onClick={handleChangePassword}>Change Password</button>
      <button className='profile-button' onClick={handleCancel}>Cancel</button>
      {isPasswordChangeSuccessful ? <p className="success-message">Password change successful</p> : null}
        {error ? <p className='error-message'>{error}</p> : null}
      </div>
    </div>
  )
}
