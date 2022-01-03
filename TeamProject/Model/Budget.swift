//
//  Budget.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 07/12/2021.
//

import Foundation
import SwiftUICharts
import SwiftUI

struct Budget: Codable, Identifiable {
	let id: Int
	let description: String
	let amount: Int
	let addedDate: Date
}

extension Budget {
	struct BudgetAPI: Codable {
		var description: String = ""
		var amount: Int = 1
		var addedData: String = Date().ISO8601Format()
	}
	
	var data: BudgetAPI {
		BudgetAPI(description: description, amount: amount, addedData: addedDate.ISO8601Format())
	}
	
	static func createBudget(from budget: BudgetAPI) -> Budget {
		var date: Date
		do {
			date = try Date(budget.addedData, strategy: .iso8601)
		} catch let error {
			print(error.localizedDescription)
			date = Date()
		}
		date += 3600 // 
		let ret =  Budget(id: Int.random(in: Int.min...Int.max - 1), description: budget.description, amount: budget.amount, addedDate: date)
		return ret
	}
	
}

extension Budget {
	func createPieData() -> PieChartDataPoint {
		return PieChartDataPoint(value: Double(self.amount), description: self.description, date: self.addedDate, colour: Color.random, label: OverlayType.label(text: self.description))
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

