import React, { useEffect, useState } from 'react'

export default function Profile() {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const[password, setPassword] = useState('');
    const [isLoading, setIsLoading] = useState(true);
    const [booksCount, setBooksCount] = useState(0);

    useEffect(() => {
fetch('https://localhost:7276/api/user/me', {credentials: 'include'})
.then((res) => res.json())
      .then((data) => {
        console.log(data);
        setIsLoading(false);
        setUsername(data.username);
        setEmail(data.email);
        setPassword(data.password);
        setBooksCount(data.booksCount);
      })
      .catch((err) => console.error("Error fetching user profile", err));
  }, []);


  const handleUpdateProfile = () => {
    
    fetch('https://localhost:7276/api/user/me', {
      method: 'PUT', 
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        username: username, 
        email: email,
        password: password, // Make sure to validate the password on the server
      }),
    })
      .then((res) => {
        if (res.ok) {
          
          console.log('Profile updated successfully');
        } else {
          
          console.error('Profile update failed');
        }
      })
      .catch((err) => console.error('Error updating user profile', err));
  };

  const handleDeleteBooks = () => {
    // Send an HTTP request to delete all books in the user's collection
    fetch('https://localhost:7276/api/delete-books', {
      method: 'DELETE', // Use the appropriate HTTP method
      credentials: 'include',
    })
      .then((res) => {
        if (res.ok) {
          // Handle successful book deletion
          console.log('Books deleted successfully');
          setBooksCount(0); // Update the UI with the new book count
        } else {
          // Handle error during deletion
          console.error('Books deletion failed');
        }
      })
      .catch((err) => console.error('Error deleting books', err));
  };

  return (
    <div>
      <h1>Profile</h1>
      {isLoading ? (
        <p>Loading...</p>
      ) : (
        <div>
          <p>Username: {username}</p>
          <p>Email: {email}</p>
          <p>Password: {password}</p>
          <p>Books saved in your Collection: {booksCount}</p>

          <button onClick={handleUpdateProfile}>Update Profile</button>
          <button onClick={handleDeleteBooks}>Delete All Books</button>
        </div>
      )}
    </div>
  );
}

