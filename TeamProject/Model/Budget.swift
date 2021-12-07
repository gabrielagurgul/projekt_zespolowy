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
    let budgetType: BudgetType
}
