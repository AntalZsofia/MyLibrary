import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router';
import './Forum.css';
import PostCard from '../../Components/PostCard/PostCard'

export default function Forum() {
  const [postTitle, setPostTitle] = useState('');
  const [content, setContent] = useState('');
  const [posts, setPosts] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    setIsLoading(true);
    fetch('https://localhost:7276/get-all-posts', { credentials: 'include' })
      .then((res) => res.json())
      .then((data) => {
        console.log(data.posts);
        setPosts(data.posts);
      })
      .catch((err) => console.error("Error fetching posts", err));
  }, [])



  const handleAddPost = async () => {
    try {
      const newPost = {
        discussionThread: postTitle,
        content: content
      }
      const response = await fetch('https://localhost:7276/create-post', {
        method: 'POST',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newPost),
      });
      if (response.ok) {
        const responseData = await response.json();
        console.log('Post added successfully:', responseData);
      } else {
        console.error('Error sending new post:', response.statusText);
      }
    }
    catch (err) {
      console.error("Error sending new post", err);
    }
  }



  const handleCancel = () => {
    navigate('/forum');
  }
  if (isLoading) {
    <div>Loading...</div>
  }
  return (
    <>
      <div className='add-post-container'>
        <div>
          <label className="add-post-label" htmlFor="postTitle">Title:</label>
          <input
            className="add-book-input"
            type="text"
            id="postTitle"
            value={postTitle}
            onChange={(e) => setPostTitle(e.target.value)}
          />
        </div>
        <label className="add-content-label" htmlFor="content">Content:</label>
        <div>
          <textarea
            className="add-content-textarea"
            type="text"
            id="content"
            value={content}
            rows={4}
            onChange={(e) => setContent(e.target.value)}
          />
        </div>
        <div className='post-buttons-container'>
          <button className="add-post-button" onClick={handleAddPost}>Add Post</button>
          <button className="cancel-button" onClick={handleCancel}>Cancel</button>
        </div>
      </div>
      <div className='post-list'>
        {posts.map((post, index) => (
          <PostCard
          key = {index}
          postTitle={post.discussionThread}
          content={post.content}
          postCreationDate = {post.postCreationDate}
          user={post.user.userName}
/>
        ))}
      </div>
    </>
  )
}
