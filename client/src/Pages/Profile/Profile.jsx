import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router';
import './Profile.css';
import Modal from '../../Components/Modal/Modal';
import ChangePassword from './ChangePassword';
import { ThemeContext } from '../../Context/ThemeProvider';
import { useContext } from 'react';

export default function Profile() {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [isLoading, setIsLoading] = useState(true);
    const [booksCount, setBooksCount] = useState(0);
    const [joinedDate, setJoinedDate] = useState('');
    const [isChangingPassword, setIsChangingPassword] = useState(false);
    const [showDeleteAllBooks, setShowDeleteAllBooks] = useState(false);
    const [showDeleteConfirmation, setShowDeleteConfirmation] = useState(false);
    const [isDeleteConfirmationVisible, setDeleteConfirmationVisible] = useState(false);
    const [userBooks, setUserBooks] = useState([]);
    const navigate = useNavigate();
    const { darkMode } = useContext(ThemeContext);

    useEffect(() => {
fetch('https://localhost:7276/api/user/me', {credentials: 'include'})
.then((res) => res.json())
      .then((data) => {
        console.log(data);
        setIsLoading(false);
        setUsername(data.username);
        setEmail(data.email);
        setBooksCount(data.booksCount);
        setJoinedDate(data.profileCreationDate);
      })
      .catch((err) => console.error("Error fetching user profile", err));
  }, []);


  const handleUpdateProfile = () => {
    navigate('/profile/update');
    
  };

  const handleDeleteBooks = () => {
    setShowDeleteAllBooks(true);
    openDeleteConfirmation();
    setShowDeleteConfirmation(true);
  }
const getAllBooks = () => {
  fetch('https://localhost:7276/all-books', {credentials: "include"})
  .then((res) => res.json())
  .then((data) => {
    console.log(data);
    setIsLoading(false);
    setUserBooks(data.books);
  })
  .catch((err) => console.error("Error fetching user books", err));
}


  const handleDeleteAllBooks = () => {
    const booksToDelete = userBooks;
    fetch('https://localhost:7276/delete-books', {
      method: 'DELETE', 
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
     
    })
      .then((res) => {
        if (res.ok) {
          
          console.log('Books deleted successfully');
          setDeleteConfirmationVisible(false);
          setShowDeleteConfirmation(false);
          setBooksCount(0); 
        } else {
          console.error('Books deletion failed');
        }
      })
      .catch((err) => console.error('Error deleting books', err));
  };
  
  const openDeleteConfirmation = () => {
    setDeleteConfirmationVisible(true);
  };
  const closeDeleteConfirmation = () => {
    setDeleteConfirmationVisible(false);
    setShowDeleteConfirmation(false);
    navigate(`/profile`);
  };
  const toggleChangePassword = () => {
    navigate('/profile/password');
};

  return (
    <div>
      {isLoading ? (
        <p>Loading...</p>
        ) : (
          <div className={`profile-card-container ${darkMode ? 'dark-mode' : ''}`}>
          <h1 className={`profile-header ${darkMode ? 'dark-mode' : ''}`}>Profile</h1>
          <p>Username: {username}</p>
          <p>Email: {email}</p>
          <p>Joined: {joinedDate.slice(0, 10)}</p>
          <p>Books saved in your Collection: {booksCount}</p>
<div className='profile-buttons-container'>
          <button className={`profile-button ${darkMode ? 'dark-mode' : ''}`} onClick={handleUpdateProfile}>Update Profile</button>
          <button className={`profile-button ${darkMode ? 'dark-mode' : ''}`} onClick={handleDeleteBooks}>Delete All Books</button>
          <button className={`profile-button ${darkMode ? 'dark-mode' : ''}`} onClick={toggleChangePassword}>
                        {isChangingPassword ? 'Cancel Change Password' : 'Change Password'}
                    </button>
</div>
                   
        
        {showDeleteAllBooks && showDeleteConfirmation && (
        <Modal onClose={closeDeleteConfirmation}>
        <h3>Are you sure you want to delete all your books?</h3>
        <button className={`yesButton ${darkMode ? 'dark-mode' : ''}`} onClick={handleDeleteAllBooks}>Yes</button>
        <button className={`noButton ${darkMode ? 'dark-mode' : ''}`} onClick={closeDeleteConfirmation}>No</button>
      </Modal>
      )}
        </div>
      )}
    </div>
  );
        }


function ChangePasswordSection() {
  const [newPassword, setNewPassword] = useState('');

  const handleChangePassword = () => {
    // Implement password change logic
    // ...
  };


  return (
    <div>
      <h2>Change Password</h2>
      <input
      className='update-profile-input'
        type="password"
        placeholder="New Password"
        value={newPassword}
        onChange={(e) => setNewPassword(e.target.value)}
      />
      <button className='profile-button' onClick={handleChangePassword}>Change Password</button>
    </div>
  );
}

