//
//  BudgetType.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 07/12/2021.
//

import Foundation

struct BudgetType: Codable, Identifiable {
    let id: Int
    let type: String
    let budget: [Budget]?
}

extension BudgetType {
    static let budgetTypeMock = BudgetType(id: 1, type: "Food", budget: Budget.listOfBudgetMock)
    static let arrayOfBudgetTypes = [budgetTypeMock,
                                     BudgetType(id: 2, type: "Home", budget: Budget.listOfBudgetMock),
                                     BudgetType(id: 3, type: "Health", budget: Budget.listOfBudgetMock),
                                     BudgetType(id: 4, type: "Family", budget: Budget.listOfBudgetMock),
                                     BudgetType(id: 5, type: "Car", budget: nil),
                                     BudgetType(id: 6, type: "Entertainment", budget: Budget.listOfBudgetMock),
                                     BudgetType(id: 7, type: "Stimulant", budget: nil),
                                     BudgetType(id: 8, type: "Investment S", budget: Budget.listOfBudgetMock),
                                     BudgetType(id: 9, type: "Investment R", budget: nil),
                                     BudgetType(id: 10, type: "Month income", budget: Budget.listOfBudgetMock),
                                     BudgetType(id: 11, type: "Budget", budget: Budget.listOfBudgetMock)]
}
