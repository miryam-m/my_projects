let score=JSON.parse(localStorage.getItem
    ('score')) ||{
       wins:0,
       losses:0,
       ties:0
     };

    updateScoreElements();

   /*if(!score)  // score===null
   {  
     score={
       wins:0,
       losses:0,
       ties:0
     };
   }*/

    let isAutoPlaying=false;
    let IntervalId ;

   function autoPlay()
    {
      if(!isAutoPlaying)
      {
        IntervalId= setInterval(function(){
        const playerMove=pickComputerMove();
          playGame(playerMove);
       },1000)
       isAutoPlaying=true;
      }else
      {
        clearInterval(IntervalId);
        isAutoPlaying=false;
      }
    }


   function playGame( playerMove )
   {
     const computerMove=pickComputerMove();
     let result= '';

     if(playerMove==='scissors')
       {
         if(computerMove==='scissors'){
         result=' Tie.'
         }else if(computerMove==='rock'){
         result='you lose.';
         }else if(computerMove==='paper'){
         result='you win!';
         }

       }else if(playerMove==='paper')
       {
         if(computerMove==='paper'){
           result=' Tie.'
           }else if(computerMove==='scissors'){
           result='you lose.';
           }else if(computerMove==='rock'){
           result='you win!';
           }

       }else if(playerMove==='rock')
       {
        if(computerMove==='rock'){
           result=' Tie.'
         }else if(computerMove==='paper'){
           result='you lose.';
         }else if(computerMove==='scissors'){
           result='you win!';
         }
       }

       if(result==='you win!'){
         score.wins++; 
        }else if(result==='you lose.'){
        score.losses++;
       } else if(result===' Tie.'){
        score.ties++;}
       
   localStorage.setItem('score',JSON.stringify(score));

   updateScoreElements();

      document.querySelector('.js-result').
      innerHTML = result;

      document.querySelector('.js-moves')
      .innerHTML=  `you
     <img src="images/${playerMove}-emoji.png"
     class="move-icon">
     <img src="images/${computerMove}-emoji.png"
     class="move-icon">
     computer`;
   }
      

     function updateScoreElements()
       {
           document.querySelector('.js-score')
    .innerHTML=`wins: ${score.wins}, losses: ${score.losses}, ties: ${score.ties}`;
       }


       
     function pickComputerMove()
       {
         const randomNumber=Math.random();
         let computerMove='';
     
     if(randomNumber>=0 && randomNumber <1/3)
     {
           computerMove = 'rock';
     }  else if(randomNumber>=1/3 && randomNumber<2/3){
           computerMove = 'paper';
     }  else if(randomNumber>=2/3 && randomNumber<1){
           computerMove = 'scissors';
     }
     return computerMove;
       }
 