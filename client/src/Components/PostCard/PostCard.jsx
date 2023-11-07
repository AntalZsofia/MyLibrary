import React, { useState } from 'react';
//import { NavLink } from "react-router-dom";
import './PostCard.css';
import ReplyCard from '../ReplyCard/ReplyCard';


const PostCard = ({ postTitle, content, postCreationDate, user }) => {
  const [replying, setReplying] = useState(false);

  const handleClickReply = () =>{
setReplying(true);
  }

    return (
        <div className="post-card-container">
          
          <div className="post-details">
            <p className="post-postTitle">Title: {postTitle}</p>
            <p className="post-content">Content: {content}</p>
            <p className="post-postCreatinDate">Posted at: {postCreationDate}</p>
            <p className="post-user">By: {user}</p> 
            
            <div className="post-buttons">
          <button className="post-like-button">Like</button>
          <button className="post-reply-button" onClick={handleClickReply}>Reply</button>
        </div>
            </div>
            {replying && (
        <ReplyCard />
      )}
          </div>
      );
};
export default PostCard;