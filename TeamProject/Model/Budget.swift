//
//  Budget.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 07/12/2021.
//

import Foundation

struct Budget: Codable, Identifiable {
	let id: Int
	let description: String
	let amount: Int
	let addedDate: Date
}

extension Budget {
	struct BudgetAPI: Codable {
		var description: String
		var amount: Int
		var addedData: String
	}
	
	var data: BudgetAPI {
		BudgetAPI(description: description, amount: amount, addedData: addedDate.ISO8601Format())
	}
}


extension Budget {
	static let budgetMock = Budget(id: 1, description: "Ciekawy wydatek", amount: 13, addedDate: Date())
	static let listOfBudgetMock = [budgetMock,
								   Budget(id: 2, description: "Jeszcze lepszy wydatek", amount: 5, addedDate: Date() - 50000),
								   Budget(id: 3, description: "Po co to zrobilem?", amount: 7, addedDate: Date() - 723000),
								   Budget(id: 4, description: "Każdy ma smaka na maka", amount: 1, addedDate: Date() + 723000),
								   Budget(id: 5, description: "Zęby są troszkę drogie", amount: 2, addedDate: Date() - 1723000),
								   Budget(id: 6, description: "Ale najważniejsze to jest mieć podniesioną głowę!", amount: 1, addedDate: Date() + 1723000)]
}

