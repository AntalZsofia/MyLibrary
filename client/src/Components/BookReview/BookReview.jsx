import React from 'react';
import { useState } from 'react';
import './BookReview.css';

const Star = ({ selected = false, onClick = f => f }) => (
    <div onClick={onClick} className={selected ? 'star-selected' : 'star-not-selected'}>
      {selected ? '★' : '☆'}
    </div>
  );


const BookReview = ({ id, title, author, genre, imageUrl, publishDate, description, readingStatus }) => {
   
    const [showReview, setShowReview] = useState(false);
    const [rating, setRating] = useState(0);
    const [review, setReview] = useState('');

    const openReview = () => {
        setShowReview(true);
    };

    const closeReview = () => {
        setShowReview(false);
    };


    const handleReviewBook = async () => {
        try {
            const bookReview = {
                id: id,
                review: review,
                rating: rating

            }
            const response = await fetch(`https://localhost:7276/add-review/${id}`, {
                method: 'PUT',
                credentials: 'include',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(bookReview),
            });
            if (response.ok) {
                const responseData = await response.json();
                console.log('Book review added successfully:', responseData); // Log the response data
                
            } else {
                console.error('Error adding review', response.statusText);
            }
        }
        catch (err) {
            console.error("Error adding book review", err);
        }
    };

    return (


        <div className="book-card">
            <img src={imageUrl} alt={`${title} cover`} className="book-image" />

            <div className='book-details'>
                <h2 className="book-title">{title}</h2>
                <p className="book-author">Author: {author} </p>
                <p className="book-genre">Genre: {genre}</p>
                <p className="book-publish-year">Published: {publishDate}</p>
                <button className="add-book-button" onClick={openReview}>Write Review</button>
                {showReview && (
                    <div className="modal-review">
                        <div className="modal-content-review">
                            
                            <form>
                                <div className='review-rating-container'>
                                    <div className='review-rating-title'>Rating</div>
                                    <div className='rating-stars'>
                                    {[...Array(5)].map((n, i) => (
                                        <Star key={i} selected={i < rating} onClick={() => setRating(i + 1)} />
                                    ))}
                                    </div>
                                    <div className='review-rating-title'>Review</div>
                                    <textarea className="review" 
                                    name="review" 
                                    rows="4" 
                                    cols="50" 
                                    placeholder="Write your review here..." 
                                    onChange={(e) => setReview(e.target.value)}></textarea>
                                 </div>
                                 <div className='review-buttons-container'>
                                    <button className="review-book-button" onClick={handleReviewBook}>Submit</button>
                                    <button className='cancel-review-button' onClick={closeReview}>Cancel</button>
                            </div>
                            </form>
                    </div>
        </div>)}
        </div>
    </div >
  
   
  );
  }
export default BookReview;