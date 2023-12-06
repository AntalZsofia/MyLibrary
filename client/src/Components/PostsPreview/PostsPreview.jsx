import React from 'react'
import PostCard from '../PostCard/PostCard'
import { ThemeContext } from '../../Context/ThemeProvider';
import { useContext } from 'react';

export default function PostsPreview( {postTitle, content, postCreationDate, user, id}) {
const { darkMode } = useContext(ThemeContext);

  return (
    <div className={`posts-preview-container ${darkMode ? 'dark-mode' : ''}`}>
          
          <div className="post-details">
            <p className="post-postTitle">Title: {postTitle}</p>
            <p className="post-content">Content: {content}</p>
            <p className="post-postCreatinDate">Posted at: {postCreationDate}</p>
            <p className="post-user">By: {user.username}</p> 
            
            <div className="post-buttons">
          <button className="post-like-button">Like</button>
          <button className="post-reply-button" onClick={<PostCard id={id}/>}>Read</button>
        </div>
            </div>
            /</div>
  )
}
