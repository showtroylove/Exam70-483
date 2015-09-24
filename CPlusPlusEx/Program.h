#include<iostream>
#include<string>

#pragma once

struct Node
{
	int data;
	struct Node *next;
};

Node* InsertNth(Node *head, int data, int position)
{
	// Complete this method only
	// Do not write main function. 
	Node* nNode = new Node();
	nNode->data = data;
	nNode->next = NULL;

	if (head == NULL)
		return head = nNode;
	if (position == 0)
		return nNode->next = head;

	int i = 0;
	Node* last = head;
	while (last->next)
	{
		if (i == position - 1) break;
		last = last->next;
		i++;
	}

	Node* tmp = last->next;
	last->next = nNode;
	nNode->next = tmp;

	return head;
}

/*
void deleteNode(struct node *head, struct node *n)
{
	// When node to be deleted is head node
	if(head == n)
	{
		if(head->next == NULL)
		{
		printf("There is only one node. The list can't be made empty ");
		return;
		}

		head->data = head->next->data;

		// store address of next node
		n = head->next;

		// Remove the link of next node
		head->next = head->next->next;

		// free memory
		free(n);

		return;
	}


	// When not first node, follow the normal deletion process

	// find the previous node
	struct node *prev = head;
	while (prev->next != NULL && prev->next != n)
		prev = prev->next;

	// Check if node really exists in Linked List
	if (prev->next == NULL)
	{
		printf("\n Given node is not present in Linked List");
		return;
	}

	// Remove node from Linked List
	prev->next = prev->next->next;

	// Free memory
	free(n);

	return;
}
 
*/

Node* Delete(Node *head, int pos)
{
	std::cout << "pos: " << pos << " data: " << head->data << std::endl;
	
	Node* p = head;
	if (head == NULL)
		return head;
	int i = 0;
	while (i < pos - 1) {
		p = p->next;
		i++;
	}

	if (p->next != NULL)
	{
		Node* del = p->next;
		p->next = del->next;
		free(del);
	}
	else
	{
		free(head);
		head = NULL;
	}

	return head;
}

using namespace std;

void Pangram()
{
	int t;
	string s;

	cin >> t;
	for (int i = 0; i < t; i++)
	{
		cin >> s;
		int del = 0;
		char chr = s[0];

		for (int itr = s.length(); itr < s.length(); itr++)
		{
			if (chr == s[itr])
				del++;
			else
				chr = s[itr];
		}

		cout << del << endl;
	}
}

void main()
{
	Pangram();
}