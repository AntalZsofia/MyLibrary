import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const BookForm = ({ bookData, onFormSubmit }) => {
  const navigate = useNavigate();
  const [title, setTitle] = useState(bookData?.title || '');
  const [author, setAuthor] = useState(bookData?.author || '');
  const [genre, setGenre] = useState(bookData?.genre || '');
  const [publishDate, setPublishDate] = useState(bookData?.publishDate || '');
  const [description, setDescription] = useState(bookData?.description || '');
  const [smallCoverImage, setSmallCoverImage] = useState(bookData?.smallCoverImage || '');


  const handleChange = (e) => {
    const { name, value } = e.target;
    switch (name) {
      case "title":
        setTitle(value);
        break;
      case "author":
        setAuthor(value);
        break;
      case "genre":
        setGenre(value);
        break;
      case "publishDate":
        setPublishDate(value);
        break;
      case "description":
        setDescription(value);
        break;
      default:
        break;
    }
  };

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
      smallCoverImage
  };
  onFormSubmit(updatedBook);
}

  return (
    <form className='add-book-container'>
      <div>
        <label>Title:</label>
        <input className="add-book-input" 
        type="text" name="title" 
        defaultValue={bookData ? bookData.title : null} 
        onChange={handleChange} />
      </div>
      <div>
        <label>Author:</label>
        <input className="add-book-input" 
        type="text" name="author" 
        defaultValue={bookData ? bookData.author : null} 
        onChange={handleChange} />
      </div>
      <div>
        <label>Genre:</label>
        <input className="add-book-input" 
        type="text" name="genre" 
        defaultValue={bookData ? bookData.genre : null} 
        onChange={handleChange} />
      </div>
      <div>
        <label>Description:</label>
        <textarea
        className="add-book-textarea"
          type="text"
          name="description"
          defaultValue={bookData ? bookData.description : null} 
          onChange={handleChange}
          rows={4}
        />
      </div>
      <div>
        <label>Published:</label>
        <input className="add-book-input" 
        type="text" 
        name="publishDate" 
        defaultValue={bookData ? bookData.publishDate : null} 
        onChange={handleChange} />
      </div>
      <div>
        <label>Cover:</label>
        <input className="add-book-cover-input"
        type="file" 
        name="imageUrl" 
        defaultValue={bookData ? bookData.smallCoverImage : null}  
        onChange={handleChange} />
      </div>
      <div className='book-form-buttons-container'>
      <button className="add-book-button"
      type="button" 
      onClick={handleFormSubmit}>
        Save
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

    
