document.addEventListener("DOMContentLoaded", function() {
    // Get category from URL (added to the link when clicking)
    const urlParams = new URLSearchParams(window.location.search);
    const category = urlParams.get('category');  // e.g., 'History'
    
    // Define the URL for fetching questions from the backend based on category
    const localUrl = `/api/questions/${encodeURIComponent(category)}`;
    
    // Fetch questions from the backend
    fetch(localUrl)
        .then(response => response.json())
        .then(data => {
            const questionsList = document.getElementById('questions-list');
            data.forEach(question => {
                const li1 = document.createElement('li');
                li1.className = 'profile__skills';
                li1.innerHTML = `
                    <div class="load-bar">
                        <a href="#" style="text-decoration: none;">
                            <div class="load-bar__bar bar--80" style="margin-left: 45px; padding: 10px;">
                                <strong>${question.option1}</strong>
                            </div>
                        </a>
                    </div>`;
                questionsList.appendChild(li1);

                const li2 = document.createElement('li');
                li2.className = 'profile__skills';
                li2.innerHTML = `
                    <div class="load-bar">
                        <a href="#" style="text-decoration: none;">
                            <div class="load-bar__bar bar--80" style="margin-left: 45px; padding: 10px;">
                                <strong>${question.option2}</strong>
                            </div>
                        </a>
                    </div>`;
                questionsList.appendChild(li2);

                const li3 = document.createElement('li');
                li3.className = 'profile__skills';
                li3.innerHTML = `
                    <div class="load-bar">
                        <a href="#" style="text-decoration: none;">
                            <div class="load-bar__bar bar--80" style="margin-left: 45px; padding: 10px;">
                                <strong>${question.option3}</strong>
                            </div>
                        </a>
                    </div>`;
                questionsList.appendChild(li3);

                const li4 = document.createElement('li');
                li4.className = 'profile__skills';
                li4.innerHTML = `
                    <div class="load-bar">
                        <a href="#" style="text-decoration: none;">
                            <div class="load-bar__bar bar--80" style="margin-left: 45px; padding: 10px;">
                                <strong>${question.option4}</strong>
                            </div>
                        </a>
                    </div>`;
                questionsList.appendChild(li4);
            });
        })
        .catch(error => console.error('Error fetching questions:', error));
});
