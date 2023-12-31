import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router';
import './Replies.css';
import ReplyCard from '../ReplyCard/ReplyCard';
import Like from './../../Icons/like.png';
import LikeDarkMode from './../../Icons/like-dark-mode.png';
import { ThemeContext } from '../../Context/ThemeProvider';
import { useContext } from 'react';

export function convertDate(timestamp) {
  const date = new Date(timestamp);

  const year = date.getFullYear();
  const month = date.toLocaleString('en-US', { month: 'long' });
  const day = date.toLocaleString('en-US', { day: 'numeric' });
  const hours = date.getHours();
  const minutes = date.getMinutes();
  const seconds = date.getSeconds();

  const formattedDate = `${year}. ${month}. ${day}. ${hours}:${minutes}:${seconds}`;

  return formattedDate;
}


export default function Replies({ discussionThread }) {
  const [replies, setReplies] = useState([]);
  const { id } = useParams();
  const [isLoading, setIsLoading] = useState(true);
  const { darkMode } = useContext(ThemeContext);

  const fetchReplies = () => {
    setIsLoading(true);
    fetch(`https://localhost:7276/get-replies/${id}`, { credentials: 'include' })
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setReplies(data);
        setIsLoading(false);
      })
      .catch((err) => console.error("Error fetching posts", err));
  };

  useEffect(() => {
    fetchReplies();
  }, [id]);

  if(isLoading){
    return <div>Loading...</div>;
  }

  return (
    <div className={`replies-container ${darkMode ? 'dark-mode' : ''}`}>
      {!replies || replies.length === 0 ? (
        <div className='no-reply-message'>Be the first one to reply</div>
      ) : (
        <>
          <ul className={`replies-list ${darkMode ? 'dark-mode' : ''}`}>
            {replies.map((reply) => (
              <li key={reply.id} className={`reply-item ${darkMode ? 'dark-mode' : ''}`}>
                <div className="reply-header">
                  <h4>Re: {discussionThread}</h4>
                </div>
                <div className="reply-content">{reply.reply}</div>
                <div className= {`reply-postCreationDate ${darkMode ? 'dark-mode' : ''}`}>Answered at: {convertDate(reply.postCreationDate)}</div>
                <div className={`reply-user ${darkMode ? 'dark-mode' : ''}`}>By: {reply.user.userName}</div>
                <div className='like-button-container'>
            <img src={darkMode ? LikeDarkMode : Like} alt='logo' className={`like ${darkMode ? 'dark-mode' : ''}`}></img>
            <p className='like-message'>Like</p>
            </div>
              </li>
            ))}
          </ul>
        </>
      )}
      <ReplyCard onReplySubmit={fetchReplies} />
    </div>
  );
}
