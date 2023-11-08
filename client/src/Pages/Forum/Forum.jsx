import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router';
import './Forum.css';
import PostCard from '../../Components/PostCard/PostCard'

export default function Forum() {
  const [postTitle, setPostTitle] = useState('');
  const [content, setContent] = useState('');
  const [posts, setPosts] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [selectedPost, setSelectedPost] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    setIsLoading(true);
    fetch('https://localhost:7276/get-all-posts', { credentials: 'include' })
      .then((res) => res.json())
      .then((data) => {
        console.log(data.posts);
        setPosts(data.posts);
        setIsLoading(false);
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
  const handleReadPost = (post) => {
    setSelectedPost(post); 
  }


  if (isLoading) {
    return <div>Loading...</div>
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
         
        </div>
      </div>
      <div className='post-list'>
        {selectedPost ? (
        <PostCard
        key = {selectedPost.id}
        postTitle={selectedPost.discussionThread}
        content={selectedPost.content}
        postCreationDate = {selectedPost.postCreationDate}
        user={selectedPost.user.userName}
        />
        ) :
        (
          // Otherwise, show the list of posts in a table
          <table className='post-table'>
            <thead>
              <tr>
                <th>Title</th>
                <th>Content</th>
                <th>User</th>
                <th>Date</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {posts.map((post) => (
                <tr key={post.id}>
                  <td>{post.discussionThread}</td>
                  <td>{post.content}</td>
                  <td>{post.user.userName}</td>
                  <td>{post.postCreationDate}</td>
                  <td>
                    <button className='read-post-button' onClick={() => <PostCard />}>Read</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
        
      </div>
    </>
  )
}
