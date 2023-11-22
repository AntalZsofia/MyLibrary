import React, { useState, useEffect } from 'react'
import { useParams, useNavigate } from 'react-router-dom'

import Modal from '../../Components/Modal/Modal';
import UpdateBook from '../UpdateBook/UpdateBook';
import './SelectedBook.css';

export default function SelectedBook() {

  const [selectedBook, setSelectedBook] = useState({});
  console.log(selectedBook);
  const [showUpdateBook, setShowUpdateBook] = useState(false);
  const [showDeleteBook, setShowDeleteBook] = useState(false);
  const [showDeleteConfirmation, setShowDeleteConfirmation] = useState(false);
  const [isDeleteConfirmationVisible, setDeleteConfirmationVisible] = useState(false);
  const [bookReadingStatus, setBookReadingStatus] = useState(selectedBook.readingStatus || "NotStarted");
console.log(bookReadingStatus);
  const navigate = useNavigate();
  const { id } = useParams();

  useEffect(() => {

    fetch(`https://localhost:7276/get-book/${id}`, { credentials: 'include'})
      .then(res => res.json())
      .then(data => {
        console.log(data);
        setSelectedBook(data);
        setBookReadingStatus(data.readingStatus || "NotStarted");
      })
      .catch(err => console.log(err))
    

  }, [id]);


  const handleDeleteBook = async () => {
    try {
      const bookToDelete = {
        title: selectedBook.title,
        author: selectedBook.author,
        genre: selectedBook.genre,
        publishYear: selectedBook.publishYear,
        description: selectedBook.description,
        imageUrl: selectedBook.imageUrl,
        readingStatus: selectedBook.readingStatus,
      };
      const response = await fetch(`https://localhost:7276/delete-book/${id}`, {
        method: 'DELETE',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(bookToDelete),
      });
      if (response.ok) {
        const responseData = await response.json();
        setDeleteConfirmationVisible(false);
        navigate('/mybooks');

        console.log('Book deleted from the collection:', responseData);

      } else {
        console.error('Error deleting book from collection:', response.statusText);
      }
    } catch (err) {
      console.error('Error deleting book from collection', err);
    }
  };

  const handleUpdateClick = () => {
    setShowUpdateBook(true);
    navigate(`/update-book/${id}`);
  };

  const handleDeleteClick = () => {
    setShowDeleteBook(true);
    openDeleteConfirmation();
    setShowDeleteConfirmation(true);
  };
  const openDeleteConfirmation = () => {
    setDeleteConfirmationVisible(true);
  };

  const closeDeleteConfirmation = () => {
    setDeleteConfirmationVisible(false);
    setShowDeleteConfirmation(false);
    navigate(`/selected-book/${id}`)
  };
console.log(id);

  const handleUpdateReadingStatus = async () => {
    try {
      const bookToChange = {
        id: id,
        readingStatus: bookReadingStatus,
      };
      const response = await fetch(`https://localhost:7276/change-reading-status/${id}`, {
        method: 'PUT',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(bookToChange),
      });
      if (response.ok) {
        const responseData = await response.json();
        console.log(bookToChange)
        console.log('Reading status updated:', responseData);
        navigate('/mybooks');
      } else {
        console.error('Error updating reading status:', response.statusText);
      }
    }
    catch (err) {
      console.error('Error updating reading status', err);
    }
  }

  return (
    <div className="selected-book-card">
      <img src={selectedBook.smallCoverImage} alt={`${selectedBook.title} cover`} className="selected-book-image" />

      <div className="selected-book-details">
        <h2 className="selected-book-title">{selectedBook.title}</h2>
        <p className="selected-book-author">Author: {selectedBook.author}</p>
        <p className="selected-book-genre">Genre: {selectedBook.genre}</p>
        <p className="selected-book-publish-year">Published: {selectedBook.publishDate}</p>
        <p className="selected-book-description">Description: {selectedBook.description}</p>
        <div className="buttons-container">
        </div>
        <button className="update-book-button" onClick={handleUpdateClick}>
          Update
        </button>
        <button className="delete-book-button" onClick={handleDeleteClick}>
          Delete
        </button>
        <div className='reading-status-container'>
          <div className='reading-status-option'>
            <input
              type="radio"
              value="NotStarted"
              name="readingStatus"
              checked={bookReadingStatus === "NotStarted"}
              onChange={(e) => setBookReadingStatus(e.target.value)}
            />
            <label htmlFor='NotStarted'>Not Started</label>
          </div>
          <div className='reading-status-option'>
            <input
              type="radio"
              value="Reading"
              name="readingStatus"
              onChange={(e) => setBookReadingStatus(e.target.value)}
            />
            <label htmlFor='Reading'>Reading</label>
            </div>
          <div className='reading-status-option'>
            <input
              type="radio"
              value="Finished"
              name="readingStatus"
              onChange={(e) => setBookReadingStatus(e.target.value)}
            />
            <label htmlFor='Finished'>Finished</label>
          </div>

          <button className="add-to-currently-reading-button" onClick={handleUpdateReadingStatus}>Update Reading Status</button>
        </div>
      </div>
      {showUpdateBook && <UpdateBook />}
      {showDeleteBook && showDeleteConfirmation && (
        <Modal onClose={closeDeleteConfirmation}>
          <h3>Are you sure you want to delete this book?</h3>
          <button className="yesButton" onClick={handleDeleteBook}>Yes</button>
          <button className="noButton" onClick={closeDeleteConfirmation}>No</button>
        </Modal>
      )}
    </div>
  )
}
