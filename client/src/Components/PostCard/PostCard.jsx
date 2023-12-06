import React, { useEffect, useState } from 'react';
import './PostCard.css';
import ReplyCard from '../ReplyCard/ReplyCard';
import { useNavigate, useParams } from 'react-router';
import Replies from '../Replies/Replies';
import Like from './../../Icons/like.png';
import useAuth from '../../Hooks/useAuth';
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

const PostCard = () => {
  const [replying, setReplying] = useState(false);
  const [post, setPost] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const { id } = useParams();
  const { user } = useAuth();
  const [postUser, setPostUser] = useState('');
  const navigate = useNavigate();
  const { darkMode } = useContext(ThemeContext);


  useEffect(() => {
    setIsLoading(true);
    fetch(`https://localhost:7276/get-post/${id}`, { credentials: 'include' })
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setPost(data);
        setIsLoading(false);
        setPostUser(data.user);
        console.log("post user: " + postUser.userName);
      })
      .catch((err) => console.error("Error fetching post", err));
  }, [id])

const handleClick = () => {
  navigate(`/update-post/${id}`);
}

  if (isLoading) {
    return <div>Loading...</div>
  }

  return (
    <>
      <div className={`post-card-container ${darkMode ? 'dark-mode' : ''}`}>

        <div className={`post-details ${darkMode ? 'dark-mode' : ''}`}>
          <p className="post-postTitle">Title: {post.discussionThread}</p>
          <p className="post-content">Content: {post.content}</p>
          <p className="post-postCreatinDate">Posted at: {convertDate(post.postCreationDate)}</p>
          <p className="post-user">By: {post.user.userName}</p>
          <div className='like-button-container'>
            <img src={Like} alt='logo' className='like'></img>
            <p className='like-message'>Like</p>
            </div>
            <div className='post-more-button-container'>
            {postUser.userName.username === user.userName && (
              <button className={`post-more-button ${darkMode ? 'dark-mode' : ''}`} onClick={handleClick}>Update</button>
            )}
            </div>
      </div>
      <div className='reply-posts-container'>
        <Replies discussionThread={post.discussionThread} />
      </div>

    </div >
      { replying && (
        <ReplyCard />
      )
}
      </>
      );
};
export default PostCard;