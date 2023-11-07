import React, { useState } from 'react';
import './ReplyCard.css';

const ReplyCard = () => {
  const [replyText, setReplyText] = useState('');

  const handleReplyTextChange = (e) => {
    setReplyText(e.target.value);
  }

  const handleReplySubmit = () => {
    // Implement your reply submission logic here
    // You can send the replyText to a server or update the UI as needed
    // After submitting, you may want to reset the reply form or close it
  }
  const handleCancel = () => {
    oncancel();
  }

  return (
    <div className="reply-form">
      <textarea
        rows="5"
        placeholder="Write your reply"
        value={replyText}
        onChange={handleReplyTextChange}
      />
      <button className="submit-reply-button" onClick={handleReplySubmit}>Submit</button>
      <button className='cancel-button' onClick={handleCancel}>Cancel</button>
    </div>
  );
};

export default ReplyCard;