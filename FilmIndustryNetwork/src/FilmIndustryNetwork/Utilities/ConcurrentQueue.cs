using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;

namespace FilmIndustryNetwork.Utilities
{

    public class ConcurrentQueue
    {
        private readonly object _movieLocker = new object();
        private readonly object _personLocker = new object();
        private readonly object _relationshipLocker = new object();

        private readonly Queue<string> _incompleteMovieQueue = new Queue<string>();
        private readonly Queue<string> _incompletePersonQueue = new Queue<string>();
        private readonly Queue<Relationship> _pendingRelationships = new Queue<Relationship>();

        public bool IncompleteMoviesWaiting()
        {
            lock (_movieLocker)
            {
                return _incompleteMovieQueue.Count > 0;
            }
        }

        public bool IncompletePersonsWaiting()
        {
            lock (_personLocker)
            {
                return _incompletePersonQueue.Count > 0;
            }
        }

        public void PushIncompleteMovie(string movie)
        {
            lock (_movieLocker)
            {
                _incompleteMovieQueue.Enqueue(movie);
            }
        }

        public string PopIncompleteMovie()
        {
            lock (_movieLocker)
            {
                return _incompleteMovieQueue.Dequeue();
            }
        }

        public string PopLastIncompleteMovie()
        {
            lock (_movieLocker)
            {
                return _incompleteMovieQueue.DequeueLast();
            }
        }

        public void PushIncompletePerson(string person)
        {
            lock (_personLocker)
            {
                _incompletePersonQueue.Enqueue(person);
            }
        }

        public string PopIncompletePerson()
        {
            lock (_personLocker)
            {
                return _incompletePersonQueue.Dequeue();
            }
        }

        public string PopLastIncompletePerson()
        {
            lock (_personLocker)
            {
                return _incompletePersonQueue.DequeueLast();
            }
        }

        public bool RelationshipsPending()
        {
            lock (_relationshipLocker)
            {
                return _pendingRelationships.Count > 0;
            }
        }

        public void PushRelationship(Relationship relationship)
        {
            lock (_relationshipLocker)
            {
                _pendingRelationships.Enqueue(relationship);
            }
        }

        public Relationship PopRelationship()
        {
            lock (_relationshipLocker)
            {
                return _pendingRelationships.Dequeue();
            }
        }

        private class Queue<E>
        {
            private readonly LinkedList<E> _list;

            public int Count => _list.Count;

            public Queue()
            {
                _list = new LinkedList<E>();
            }

            public void Enqueue(E item)
            {
                _list.AddLast(item);
            }

            public E Dequeue()
            {
                var e = _list.First.Value;
                _list.RemoveFirst();
                return e;
            }

            public E DequeueLast()
            {
                var e = _list.Last.Value;
                _list.RemoveLast();
                return e;
            }
        }
    }

}
