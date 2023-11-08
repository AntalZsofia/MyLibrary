import React, { useEffect, useState } from 'react';
//import { NavLink } from "react-router-dom";
import './PostCard.css';
import ReplyCard from '../ReplyCard/ReplyCard';
import { useNavigate, useParams } from 'react-router';


const PostCard = ({ postTitle, content, postCreationDate, user, id }) => {
  const [replying, setReplying] = useState(false);
  const [post, setPost] = useState('');
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(true);
  const { id } = useParams();
  
  useEffect(() => {
    setIsLoading(true);
    fetch(`https://localhost:7276/get-post/${id}`, { credentials: 'include' })
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setPost(data);
        setIsLoading(false);
      })
      .catch((err) => console.error("Error fetching post", err));
  }, [])
  const handleClickReply = () =>{
setReplying(true);
  }

  const handleCancel = () => {
    navigate('/forum');
  }

    return (
        <div className="post-card-container">
          
          <div className="post-details">
            <p className="post-postTitle">Title: {post.postTitle}</p>
            <p className="post-content">Content: {post.content}</p>
            <p className="post-postCreatinDate">Posted at: {post.postCreationDate}</p>
            <p className="post-user">By: {post.user.username}</p> 
            
            <div className="post-buttons">
          <button className="post-like-button">Like</button>
          <button className="post-reply-button" onClick={handleClickReply}>Reply</button>
          <button className="cancel-button" onClick={handleCancel}>Cancel</button>
        </div>
            </div>
            {replying && (
        <ReplyCard
        id={id} />
      )}
          </div>
      );
};
export default PostCard;