import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const BookForm = ({ book, onFormSubmit }) => {
  const navigate = useNavigate();
  console.log(book);
  const [title, setTitle] = useState(book?.title || '');
  const [author, setAuthor] = useState(book?.author || '');
  const [genre, setGenre] = useState(book?.genre || '');
  const [publishDate, setPublishDate] = useState(book?.publishDate || '');
  const [description, setDescription] = useState(book?.description || '');
  const [smallCoverImage, setSmallCoverImage] = useState(book?.smallCoverImage || '');


  const handleCancel = () => {
    navigate('/mybooks');
  }

  const handleFormSubmit = () => {
    const updatedBook = {
      title,
      author,
      genre,
      publishDate,
      description,
      smallCoverImage,
    };
  
    onFormSubmit(updatedBook);
    navigate('/mybooks');
  };

  return (
    <form className='add-book-container'>
      <div>
        <label>Title:</label>
        <input className="add-book-input" 
        type="text" name="title" 
        value={title}
        onChange={e => setTitle(e.target.value)}
         />
      </div>
      <div>
        <label>Author:</label>
        <input className="add-book-input" 
        type="text" name="author" 
        value={author} 
        onChange={e => setAuthor(e.target.value)}
       />
      </div>
      <div>
        <label>Genre:</label>
        <input className="add-book-input" 
        type="text" name="genre" 
        value={genre} 
        onChange={e => setGenre(e.target.value)}
         />
      </div>
      <div>
        <label>Description:</label>
        <textarea
        className="add-book-textarea"
          type="text"
          name="description"
          value={description} 
          onChange={e => setDescription(e.target.value)}
          rows={4}
        />
      </div>
      <div>
        <label>Published:</label>
        <input className="add-book-input" 
        type="text" 
        name="publishDate" 
        value={publishDate} 
        onChange={e => setPublishDate(e.target.value)}
         />
      </div>
      <div>
        <label>Cover:</label>
        <input className="add-book-cover-input"
        type="text" 
        name="imageUrl" 
        value={smallCoverImage}
        onChange={e => setSmallCoverImage(e.target.value)}  
         />
      </div>
      <div className='book-form-buttons-container'>
      <button className="add-book-button"
      type="button" 
      onClick={handleFormSubmit}>
        Update
      </button>
      <button className="add-book-button"
      type="button" 
      onClick={handleCancel}>
        Cancel
      </button>
      </div>
    </form>
  );
};

export default BookForm;

    
