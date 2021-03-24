# Sedir
* Returns an ack after persisting a document to both master and replica node.
* Resident should be configurable such as FIFO.
* Keeps indexes on node where corresponding to the document is stored in.
* Persists on the disk and then asynchronously load into the RAM
* All documents should be replicated at least on one another node (or exactly one another node???)
* Sharding algorithm is another question. But it will be handled on the server side. Not client side.
* It sacrifices availability during network partition. So it's CP.
* ACID?
* Leader election or leaderless model?