import React, { useState } from 'react';
import './ReplyCard.css';
import { useNavigate, useParams } from 'react-router';
import { ThemeContext } from '../../Context/ThemeProvider';
import { useContext } from 'react';

const ReplyCard = ({ onReplySubmit }) => {
  const [replyText, setReplyText] = useState('');
  const navigate = useNavigate();
  const { id } = useParams();
  const { darkMode } = useContext(ThemeContext);

  const handleReplySubmit = async () => {
    try {

      const newReply = {
        reply: replyText
      }
      const response = await fetch(`https://localhost:7276/create-reply/${id}`, {
        method: 'POST',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newReply),
      });
      if (response.ok) {
        const responseData = await response.json();
        console.log('Reply added successfully:', responseData);
        onReplySubmit();
        setReplyText('');
      } else {
        console.error('Error sending new reply:', response.statusText);
      }
    }
    catch (err) {
      console.error("Error sending new reply", err);
    };
  }

  const handleCancel = () => {
    navigate('/forum');
  }

  return (
    <div className={`reply-form ${darkMode ? 'dark-mode' : ''}`}>
      <h4>New Reply</h4>
      <textarea
      className={`reply-textarea ${darkMode ? 'dark-mode' : ''}`}
        rows="5"
        placeholder="Write your reply"
        value={replyText}
        onChange={(e) => setReplyText(e.target.value)}
      />
      <div className="submit-cancel-buttons">
        <button className={`submit-reply-button ${darkMode ? 'dark-mode' : ''}`} onClick={handleReplySubmit}>Submit</button>
        <button className={`cancel-button ${darkMode ? 'dark-mode' : ''}`} onClick={handleCancel}>Cancel</button>
      </div>

    </div>
  );
};

export default ReplyCard;