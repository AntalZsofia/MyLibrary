import React from 'react';
import './BookCardColl.css';
import { NavLink } from "react-router-dom";
import openBook from './../../Icons/open-book.png';
import checked from './../../Icons/checked.png';

const Star = ({ selected = false, onClick = f => f }) => (
  <div onClick={onClick} className={selected ? 'star-selected' : 'star-not-selected'}>
    {selected ? '★' : '☆'}
  </div>
);


const BookCardColl = ({ title, author, genre, imageUrl, publishYear, description, id, readingStatus, rating }) => {




  return (
    <div className="book-card">
      <div className="image-and-rating-container">
      <img src={imageUrl} alt={`${title} cover`} className="book-image" />

          <div className="rating-container">
            <span className="star-container">

              {[...Array(5)].map((e, i) => <Star key={i} selected={i < rating} />)}
            </span>
          </div>
      </div>
      <div className="book-details">
        <h2 className="book-title">{title}</h2>
        <p className="book-author">Author: {author}</p>
        <p className="book-genre">Genre: {genre}</p>
        <p className="book-publish-year">Published: {publishYear}</p>
        <div className="bottom-container">

          
            <div className="buttons-container">
              {readingStatus === 'Reading' && <img src={openBook} alt="Reading" style={{ width: '40px', height: '40px' }} />}
              {readingStatus === 'Finished' && <img src={checked} alt="Finished" style={{ width: '40px', height: '40px' }} />}
              <NavLink to={`/selected-book/${id}`}>
                <button className="more-book-button">More</button>
              </NavLink>
            </div>
       
        </div>
      </div>

    </div>
  );
};

export default BookCardColl;


