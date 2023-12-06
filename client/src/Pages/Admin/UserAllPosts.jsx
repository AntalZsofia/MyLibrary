import React from 'react'
import { useParams, useNavigate } from 'react-router';
import './UserAllPosts.css';
import { useState, useEffect } from 'react';
import Modal from '../../Components/Modal/Modal';
import { convertDate } from '../Admin/Admin';
import { ThemeContext } from '../../Context/ThemeProvider';
import { useContext } from 'react';

export default function UserAllPosts() {
  const [isLoading, setIsLoading] = useState(true);
  const [posts, setPosts] = useState([]);
  const [user, setUser] = useState('');
  const [showDeleteConfirmation, setShowDeleteConfirmation] = useState(false);
  const [postId, setPostId] = useState(null);  
  const { id } = useParams();
  const navigate = useNavigate();
  const { darkMode } = useContext(ThemeContext);
 
  useEffect(() => {
    setIsLoading(true);
    fetch(`https://localhost:7276/allposts/${id}`, { credentials: 'include' })
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setPosts(data);
        setIsLoading(false);
        setUser(data[0].user.userName);
        
      })
      .catch((err) => console.error("Error fetching posts", err));
  }, [id]);

  const handleDeleteClick = (postId) => {
    setPostId(postId);
    setShowDeleteConfirmation(true);
  };
 
  const closeDeleteConfirmation = () => {
    setShowDeleteConfirmation(false);
  };

  const handleDeletePost = async () => {
    try {
      const postToDelete = {
        id: posts[0].id,
        discussionThread: posts[0].discussionThread,
        content: posts[0].content,
        likes: posts[0].likes,
        postCreationDate: posts[0].postCreationDate,
        user: posts[0].user,
      };

      const response = await fetch(`https://localhost:7276/deletepost/${posts[0].id}`, {
        method: 'DELETE',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(postToDelete),
      });
      if (response.ok) {
        const responseData = await response.json();
            setShowDeleteConfirmation(false);
        navigate(`/admin`);
        console.log('Post deleted from the forum:', responseData);
      } else {
        console.error("Error deleting post", response.statusText);
      }
    } catch (err) {
      console.error("Error deleting post", err);
    }
    closeDeleteConfirmation();
  };


  if (isLoading) {
    return <div>Loading...</div>;
  }
  return (
    <div className={`-allposts-table-container ${darkMode ? 'dark-mode' : ''}`}>
      <h2>Posts by {user} </h2>
      <table className={`user-allposts-table ${darkMode ? 'dark-mode' : ''}`}>
        <thead>
          <tr>
            <th>Discussion Thread</th>
            <th>Content</th>
            <th>Posted at</th>
            <th>Likes</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody className={`user-allposts-table-data ${darkMode ? 'dark-mode' : ''}`}>
          {!posts || posts.length === 0 ? (<div>User has no posts yet</div>)
          :
          (posts.map((post, index) => (
            <tr key={index}>
              <td>{post.discussionThread}</td>
              <td>{post.content}</td>
              <td>{convertDate(post.postCreationDate)}</td>
              <td>{post.likes}</td>
              <td>
                  <button className={`admin-table-button ${darkMode ? 'dark-mode' : ''}`} onClick={handleDeleteClick}>Delete</button>
              </td>
            </tr>
          )))}
        </tbody>
      </table>
      {showDeleteConfirmation && (
        <Modal onClose={closeDeleteConfirmation}>
        <h3>Are you sure you want to delete this post?</h3>
        <button className="yesButton" onClick={handleDeletePost}>Yes</button>
        <button className="noButton" onClick={closeDeleteConfirmation}>No</button>
      </Modal>
      )}
    </div>
  )
}
