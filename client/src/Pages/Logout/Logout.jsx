import React, { useState } from 'react'
import { useNavigate } from 'react-router';
import './Logout.css';
import useAuth from '../../Hooks/useAuth';



const Logout = () => {
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false);
    const { setUser } = useAuth();
  
    const handleLogout = async () => {
      setIsLoading(true);
  
      try {
        const response = await fetch('https://localhost:7276/api/logout', {
          method: 'POST',
          credentials: 'include',
        });
  
        if (response.ok) {
          setUser(null);
          navigate('/login');
          console.log("Logged out successfully");
        } else {
          console.error('Logout failed');
        }
      } catch (error) {
        console.error('Logout failed:', error);
      } finally {
        setIsLoading(false);
      }
    };
  
    return (
      <div className="logout-container">
        <h2>Logout</h2>
        <p>Are you sure you want to log out?</p>
        <button
          className={`logout-button ${isLoading ? 'loading' : ''}`}
          onClick={handleLogout}
          disabled={isLoading}
        >
          {isLoading ? 'Logging out...' : 'Logout'}
        </button>
      </div>
    );
  };
  
  export default Logout;